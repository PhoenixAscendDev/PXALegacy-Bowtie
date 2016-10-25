using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

using JB2.Common;
using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public class ApplicationRepository : BowtieRepository<IApplication>, IApplicationRepository
    {
        

        #region Constructors

        public ApplicationRepository() : this(AzureStorage.ApplicationTable, AzureStorage.GeneralBlob)
        {
            
        }

        public ApplicationRepository(Common.Data.AzureTableRepository table, Common.Data.AzureBlobRepository blob)
        {
            _table = table;
            _blob = blob;
            _defaultPartitionKey = "application";
        }

        #endregion Constructors

        #region Gets

        public IApplication[] GetAPIAllowedApps()
        {
            var all = this.GetAll();

            return all.ToList().FindAll(x => x.isAuthorized == true).ToArray();

        }

        public IApplication[] GetApplicationsByClientID(string clientID)
        {
            var all = this.GetAll();
            return all.ToList().FindAll(x => x.ClientID == clientID).ToArray();
        }

        public IApplication GetApplicationByAPIKey(string key)
        {
            var all = this.GetAll();
            return all.ToList().Find(x => x.APIkey == key);
        }

        

        public string GetTreasuryRequestKey(string applicationID, string treasuryID)
        {
            var e = _table.GetEntity<DynamicTableEntity>("application", applicationID);


            if (e == null)
                return string.Empty;

            if (treasuryID == "jBean")
            {
                return e.Properties.ContainsKey("JBeanRequestKey") ? e.Properties["JBeanRequestKey"].StringValue : string.Empty;
            }
            else
                return string.Empty;
        }

        #endregion Gets


        #region Private Methods

        protected override DynamicTableEntity convertToEntity(IApplication o)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<IApplication> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            var result = new List<IApplication>();

            foreach(var e in list)
            {
                result.Add(convertToObject(e));
            }

            return result;
        }

        protected  ApplicationStatePair convertToState(DynamicTableEntity e)
        {
            var apiKey = new JB2.Common.ApiKeySecretPair();

            apiKey.APIkey = e.Properties["APIKey"].StringValue;
            apiKey.Secret = e.Properties["APISecret"].PropertyType == EdmType.Guid ? e.Properties["APISecret"].GuidValue.GetValueOrDefault().ToString() : e.Properties["APISecret"].PropertyAsObject.ToString();

            Enum.APIAuthorizeState state = Enum.APIAuthorizeState.Unknown;
            System.Enum.TryParse<Enum.APIAuthorizeState>(e.Properties["AuthorizeState"].StringValue, out state);
            var appID = e.GetPropertyValue<string>("ID", string.Empty);

            var result = new ApplicationStatePair(appID, state);
            result.APIKey = apiKey;

            return result;
        }

        protected override IApplication convertToObject(DynamicTableEntity e)
        {
            var authrepo = _uofw.AuthorizeRepository;

            var appstate = authrepo.GetApplicationStateByID(e.GetPropertyValue<string>("ID", string.Empty));
            var apiKey = appstate.APIKey;


            Enum.APIAuthorizeState state = appstate.AuthorizeState;


            //set the treasury keys
            List<TreasuryRequestKey> treasuryKeys = new List<TreasuryRequestKey>();
            var strtreasuryKeys = e.GetPropertyValue<string>("TreasuryKeys", string.Empty);
            if (!string.IsNullOrEmpty(strtreasuryKeys))
            {
                foreach (string keypair in strtreasuryKeys.Split(","))
                {
                    try
                    {
                        var treasuryID = keypair.Split(":")[0];
                        var requestKey = keypair.Split(":")[1];

                        var result = new TreasuryRequestKey(treasuryID, requestKey);

                        treasuryKeys.Add(result);
                    }

                    catch (Exception ex)
                    {
                        ex.BowtieLog();
                    }
                }
            }

            //System.Enum.TryParse<Enum.APIAuthorizeState>(e.Properties["AuthorizeState"].StringValue, out state);
            //List<IModule> modules = JB2.Settings.Bowtie.UnitOfWork.ModuleRepository.GetAll().ToList();

            List<IModule> modules = new List<IModule>();


            var app = new Application(apiKey.APIkey, apiKey.Secret, state,modules);
            app.ID = e.Properties["ID"].StringValue;
            app.ClientID = e.Properties.ContainsKey("IdentityClientIDs") ? e.Properties["IdentityClientIDs"].StringValue : string.Empty;
            app.Name = e.Properties["Name"].StringValue;
            app.TreasuryKeys = treasuryKeys;
            return app;

        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            throw new NotImplementedException();
        }

        #endregion Private Methods

        #region Search Methods

        public override IApplication[] SearchFor(bool useCache = true)
        {
            throw new NotImplementedException();
        }

        public override IApplication[] SearchFor(string filter, bool useCache = true)
        {
            throw new NotImplementedException();
        }

        #endregion Search Methods


    }
}
