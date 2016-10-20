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
    public class ModuleRepository : BowtieRepository<IModule>, IModuleRepository
    {
        #region Constructors
        public ModuleRepository() : this("modules","general")
        {

        }

        #endregion Constructors

        protected ModuleRepository(string tableName, string blobName)
            : this(JB2.Infrastructure.Storage.BowtieAccount.GetTable(tableName),
                   JB2.Infrastructure.Storage.BowtieAccount.GetBlog(blobName))
        {

        }

        public ModuleRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob)
        {
            _table = azureTable;
            _blob = azureBlob;
            _defaultPartitionKey = "module";
        }



        public IModule GetByName(string id)
        {
            return GetById(id, _useCache);
        }

        public IModule GetByName(string name, bool useCache = true)
        {
            //if (useCache)
            //{
            //    if (_cache.ContainsKey(id))
            //        return _cache[id];
            //}
            var e = _table.GetEntity<DynamicTableEntity>(_defaultPartitionKey, "name:" + name);

            if (e != null)
            {
                //go ahead and update cache in case the next request wants to use
                //if (_cache.ContainsKey())
                //    _cache[id] = convertToObject(e);
                //else
                //    _cache.Add(id, convertToObject(e));

                return convertToObject(e);
            }
            else
                return default(IModule);
        }

        protected override DynamicTableEntity convertToEntity(IModule o)
        {
            DynamicTableEntity e = new DynamicTableEntity();

            e.SetProperty<string>("ID", o.GetID());
            e.SetProperty<string>("Name", o.GetName());
            e.SetProperty<string>("API_Version", o.API.Version);
            e.SetProperty<string>("API_URL", o.API.URL);
            e.SetProperty<string>("ModuleStatus", o.Status.ToString());

            return e;
        }

        protected override IEnumerable<IModule> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            List<IModule> result = new List<IModule>();

            foreach(var e in list)
            {
                result.Add(convertToObject(e));
            }

            return result;
        }

        protected override IModule convertToObject(DynamicTableEntity e)
        {
            Enum.ModuleType moduleType = (Enum.ModuleType)System.Enum.Parse(typeof(Enum.ModuleType),e.GetPropertyValue<string>("ModuleType","REST"));
            var id = e.GetPropertyValue<string>("ID", string.Empty);
            IModule result = null;

            switch (moduleType)
            {
                case Enum.ModuleType.REST:
                default:
                    result = new RESTModule(id);
                    break;
            }

            Enum.ModuleStatusType statusType = Enum.ModuleStatusType.Online;
            System.Enum.TryParse(e.GetPropertyValue<string>("StatusType", string.Empty), out statusType);

            result.Name = e.GetPropertyValue<string>("Name", string.Empty);
            result.ModuleType = moduleType;        
            result.Status = statusType;

            BowtieAPI api = new BowtieAPI();
            api.URL = e.GetPropertyValue<string>("API_URL", string.Empty);
            api.Version = e.GetPropertyValue<string>("API_Version", string.Empty);

            result.API = api;

            return result;
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            _table.DeleteAllByPartitionKey(_defaultPartitionKey);
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {


            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID",string.Empty);
            _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "name:" + e.GetPropertyValue<string>("Name", string.Empty);
            _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);
        }
    }
}
