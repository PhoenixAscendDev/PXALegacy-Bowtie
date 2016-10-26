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


        #region Inserts
        public override void Insert(IApplication obj)
        {
            base.Insert(obj);

            ApplicationStatePair asp = new ApplicationStatePair();
            asp.APIKey = new ApiKeySecretPair();
            asp.APIKey.APIkey = obj.APIkey;
            asp.APIKey.Secret = obj.Secret;
            asp.ApplicationID = obj.ID;
            asp.AuthorizeState = obj.AuthorizedState;

            _uofw.AuthorizeRepository.Insert(asp);

        }


        #endregion Inserts

        #region Private Methods

        protected override DynamicTableEntity convertToEntity(IApplication o)
        {

            DynamicTableEntity e = new DynamicTableEntity();
            e.SetProperty<string>("ID", o.ID);

            //set Treasury Keys
            List<string> keys = new List<string>();

            TreasuryRequestKey jBeanKey = o.GetTreasuryRequestKey("jBean");           
            keys.Add(jBeanKey.TreasuryID + ":" + jBeanKey.Key);

            e.SetProperty<string>("TreasuryKeys", string.Join(",", keys.ToArray()));
            e.SetProperty<string>("IdentityClientIDs", o.ClientID);
            e.SetProperty<string>("UniqueToken", o.UniqueToken);
            e.SetProperty<string>("Website", o.Website);
            e.SetProperty<string>("Name", o.Name);


 
            e.SetProperty<string>("CompanyID", o.Company.ID);
            e.SetProperty<string>("CompanyName", o.Company.Name);

            
            //System.Enum.TryParse<Enum.APIAuthorizeState>(e.Properties["AuthorizeState"].StringValue, out state);
            //List<IModule> modules = JB2.Settings.Bowtie.UnitOfWork.ModuleRepository.GetAll().ToList();

            List<IModule> modules = new List<IModule>();

            return e;
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

        protected override IApplication convertToObject(DynamicTableEntity e)
        {
            var authrepo = _uofw.AuthorizeRepository;

            var appstate = authrepo.GetApplicationStateByID(e.GetPropertyValue<string>("ID", string.Empty));
            var apiKey = appstate.APIKey;


            Enum.APIAuthorizeState state = appstate.AuthorizeState;

            //set Company
            var company = new JB2.Common.Business();
            company.ID = e.GetPropertyValue<string>("CompanyID", string.Empty);
            company.Name = e.GetPropertyValue<string>("CompanyName", string.Empty);


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
            app.ID = e.GetPropertyValue<string>("ID", string.Empty);// e.Properties["ID"].StringValue;
            app.ClientID = e.GetPropertyValue<string>("IdentityClientIDs", string.Empty);// e.Properties.ContainsKey("IdentityClientIDs") ? e.Properties["IdentityClientIDs"].StringValue : string.Empty;
            app.Name = e.GetPropertyValue<string>("Name", string.Empty); // e.Properties["Name"].StringValue;
            app.TreasuryKeys = treasuryKeys;
            app.Website = e.GetPropertyValue<string>("Website", string.Empty);
            app.Company = company;
            
            return app;

        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, replace);

            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, replace);
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
