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

    public class AuthorizeRepository : IAuthorizeRepository
    {

        protected string _defaultPartitionKey;
        
        protected Common.Data.AzureTableRepository _table;
        protected Common.Data.AzureBlobRepository _blob;

        #region Constructors
        public AuthorizeRepository() : this(AzureStorage.AuthorizeTable, AzureStorage.GeneralBlob)
        {

        }

        public AuthorizeRepository(Common.Data.AzureTableRepository table, Common.Data.AzureBlobRepository blob)
        {
            _table = table;
            _blob = blob;
            _defaultPartitionKey = "application";

            
        }
        #endregion Constructors

        public ApplicationStatePair GetApplicationStateByAPIKey(string publicKey, string secret)
        {
            DynamicTableEntity e = _table.GetEntity<DynamicTableEntity>(_defaultPartitionKey, "key:" + publicKey);

            try
            {
                return convertToState(e);
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new ApplicationStatePair(string.Empty, Enum.APIAuthorizeState.Unknown);
            }
        }

        public ApplicationStatePair GetApplicationStateByAuthorizeKey(string key)
        {
            DynamicTableEntity e = _table.GetEntity<DynamicTableEntity>("accesskey:application", "accesskey:" + key);
            try
            {
                var appID = e.GetPropertyValue<string>("ID", string.Empty);

                return GetApplicationStateByID(appID);
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new ApplicationStatePair(string.Empty, Enum.APIAuthorizeState.Unknown);
            }
        }

        public ApplicationStatePair GetApplicationStateByID(string id)
        {
            DynamicTableEntity e = _table.GetEntity<DynamicTableEntity>(_defaultPartitionKey, "id:" + id);
            try
            {
                return convertToState(e);
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new ApplicationStatePair(string.Empty, Enum.APIAuthorizeState.Unknown);
            }
        }

        public ServiceResult InsertAuthorizeKey(string key, string applicationID, DateTime dateGenerated)
        {       
            try
            {
                DynamicTableEntity e = new DynamicTableEntity();
                e.PartitionKey = "accesskey:application";
                e.RowKey = "accesskey:" + key;
                e.SetProperty<string>("ID", applicationID);
                e.SetProperty<string>("Ticks", dateGenerated.Ticks.ToString());
                e.SetProperty<string>("AuthorizeKey", key);

                _table.Insert<DynamicTableEntity>(e, true);

                return true;
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new ServiceResult(ex);
            }
        }

        public ServiceResult Insert(ApplicationStatePair pair)
        {
            try
            {
                DynamicTableEntity e = new DynamicTableEntity();
                e.SetProperty<string>("APIKey", pair.APIKey.APIkey);
                e.SetProperty<string>("APISecret", pair.APIKey.Secret);
                e.SetProperty<string>("ID", pair.ApplicationID);


                e.PartitionKey = _defaultPartitionKey;
                e.RowKey = "key:" + pair.APIKey.APIkey;
                _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

                e.PartitionKey = _defaultPartitionKey;
                e.RowKey = "id:" + pair.ApplicationID;
                _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

                return true;
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return new ServiceResult(ex);
            }
        }

        public JB2.Common.ServiceResult UpdateAuthorizeState(Enum.APIAuthorizeState state, string applicationID)
        {
            try
            {
                var e = _table.GetEntity<DynamicTableEntity>(_defaultPartitionKey, "id:" + applicationID);

                var apikey = e.GetPropertyValue<string>("APIKey", string.Empty);
                var apisecret = e.GetPropertyValue<string>("APISecret", string.Empty);
                DateTime changeDate = System.DateTime.Now;
                Enum.APIAuthorizeState currentState = Enum.APIAuthorizeState.Unknown;
                System.Enum.TryParse<Enum.APIAuthorizeState>(e.GetPropertyValue<string>("AuthorizeState",string.Empty), out currentState);

                e.SetProperty<string>("AuthorizeState", state.ToString());

                if (currentState != state)
                {
                    e.SetProperty<DateTime>("StateLastChanged", changeDate);

                    //save entity
                    e.PartitionKey = _defaultPartitionKey;
                    e.RowKey = "key:" + apikey;
                    _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

                    e.PartitionKey = _defaultPartitionKey;
                    e.RowKey = "id:" + applicationID;
                    _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

                    //save change log
                    DynamicTableEntity loge = new DynamicTableEntity();
                    loge.PartitionKey = "changelog:application:" + applicationID;
                    loge.RowKey = "property:" + "authorizestate" + ":tick:" + changeDate.Ticks.ToString();
                    loge.SetProperty<DateTime>("ChangeDate", changeDate);
                    loge.SetProperty<string>("Text", "State: " + currentState.ToString() + " -> " + state.ToString());
                    loge.SetProperty<string>("PropertyName", "AuthorizeState");
                    loge.SetProperty<string>("PreviousValue", currentState.ToString());
                    loge.SetProperty<string>("NextValue", state.ToString());
                    _table.Insert<DynamicTableEntity>(loge, true);
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new ServiceResult(ex);
            }
        }

        #region Private Methods
        protected ApplicationStatePair convertToState(DynamicTableEntity e)
        {
            var apiKey = new JB2.Common.ApiKeySecretPair();

            apiKey.APIkey = e.Properties["APIKey"].StringValue;
            apiKey.Secret = e.Properties["APISecret"].PropertyType == EdmType.Guid ? e.Properties["APISecret"].GuidValue.GetValueOrDefault().ToString() : e.Properties["APISecret"].PropertyAsObject.ToString();

            Enum.APIAuthorizeState state = Enum.APIAuthorizeState.Unknown;
            System.Enum.TryParse<Enum.APIAuthorizeState>(e.GetPropertyValue<string>("AuthorizeState",string.Empty), out state);
            var appID = e.GetPropertyValue<string>("ID", string.Empty);

            var result = new ApplicationStatePair(appID, state);
            result.APIKey = apiKey;

            return result;
        }

        public long GetAuthorizeKeyTicks(string authorizeKey)
        {
            long result = 0;
            try
            {
                DynamicTableEntity e = _table.GetEntity<DynamicTableEntity>("accesskey:application", "accesskey:" + authorizeKey);
                var ticks = e.GetPropertyValue<string>("Ticks", "0");

                long.TryParse(ticks, out result);
                return result;
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return result;
            }

        }
        #endregion Private Methods
    }
}
