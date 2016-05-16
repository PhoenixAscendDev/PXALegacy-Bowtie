using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Bowtie.Data.Azure
{
    public class ApplicationRepository : BowtieRepository<IApplication>, IApplicationRepository
    {

        #region

        public ApplicationRepository() : this(AzureStorage.ApplicationTable, AzureStorage.GeneralBlob)
        {
            
        }

        public ApplicationRepository(Common.Data.AzureTableRepository table, Common.Data.AzureBlobRepository blob)
        {
            _table = table;
            _blob = blob;
            _defaultPartitionKey = "application";
        }

        #endregion


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

        

        #endregion Gets

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

        protected override IApplication convertToObject(DynamicTableEntity e)
        {
            var apiKey = new JB2.Common.ApiKeySecretPair();

            apiKey.APIkey = e.Properties["APIKey"].StringValue;


            apiKey.Secret = e.Properties["APISecret"].PropertyType == EdmType.Guid ? e.Properties["APISecret"].GuidValue.GetValueOrDefault().ToString() : e.Properties["APISecret"].PropertyAsObject.ToString();
         
            Enum.APIAuthorizeState state = Enum.APIAuthorizeState.Unknown;

            System.Enum.TryParse<Enum.APIAuthorizeState>(e.Properties["AuthorizeState"].StringValue, out state);

            var app = new Application(apiKey.APIkey, apiKey.Secret, state);
            app.ID = e.Properties["ID"].StringValue;
            app.ClientID = e.Properties.ContainsKey("IdentityClientIDs") ? e.Properties["IdentityClientIDs"].StringValue : string.Empty;

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
    }
}
