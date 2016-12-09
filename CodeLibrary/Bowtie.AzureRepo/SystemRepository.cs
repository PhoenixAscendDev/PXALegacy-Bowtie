using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using Microsoft.WindowsAzure.Storage.Table;

using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public class SystemRepository : BowtieRepository<JB2.Bowtie.ISystemConfig>, ISystemRepository
    {
        #region Constructors

        public SystemRepository() : this(AzureStorage.SystemTable,AzureStorage.GeneralBlob,AzureStorage.SystemTable)
        {

        }

        public SystemRepository(Common.Data.AzureTableRepository table, Common.Data.AzureBlobRepository blob, Common.Data.AzureTableRepository playerdata)
        {
            _table = table;
            _blob = blob;
            _defaultPartitionKey = "systemclass";
            _playerData = playerdata;
        }


        #endregion Constructors


        public IEnumerable<ISystemConfig<IPointSystem>> GetPointSystems()
        {
            var elist = _table.GetByRowKeyStartWith<DynamicTableEntity>(_defaultPartitionKey + ":category:POINT", "id:", 1000);

            List<ISystemConfig<IPointSystem>> result = new List<ISystemConfig<IPointSystem>>();

            foreach (var e in elist)
            {
                result.Add(convertToPointConfig(e));
            }

            return result;
        }

        public IEnumerable<ISystemConfig<ICurrencySystem>> GetCurrencySystems()
        {
            var elist = _table.GetByRowKeyStartWith<DynamicTableEntity>(_defaultPartitionKey + ":category:CURRENCY", "id:", 1000);

            List<ISystemConfig<ICurrencySystem>> result = new List<ISystemConfig<ICurrencySystem>>();

            foreach(var e in elist)
            {
                result.Add(convertToCurrencyConfig(e));
            }

            return result;

        }



        public ISystemConfig<ICurrencySystem> GetCurrencySystemByConfigID(string configID)
        {
            var e = _table.GetEntity<DynamicTableEntity>(_defaultPartitionKey, "id:" + configID);

            return convertToCurrencyConfig(e);
        }

        public ISystemConfig<IPointSystem> GetPointSystemByConfigID(string configID)
        {
            var e = _table.GetEntity<DynamicTableEntity>(_defaultPartitionKey, "id:" + configID);

            return convertToPointConfig(e);
        }


        public IEnumerable<ISystemConfig> GetSystemsByCategory(string category)
        {
            var elist = _table.GetByRowKeyStartWith<DynamicTableEntity>(_defaultPartitionKey + ":category:" + category.ToUpper(), "id:", 1000);

            return convertToObject(elist);

        }




        #region helpers

        protected ISystemConfig<IPointSystem> convertToPointConfig(DynamicTableEntity e)
        {
            var c = convertToObject(e);
            var result = new SystemConfig<IPointSystem>();

            result.Assembly = c.Assembly;
            result.AssemblyQualifiedName = c.AssemblyQualifiedName;
            result.Category = c.Category;
            result.ClassConfigID = c.ClassConfigID;
            result.Classname = c.Classname;
            result.ClassType = Common.Enum.ClassType.NewInstance;
            result.ConstructorParameters = c.ConstructorParameters;
            result.Namespace = c.Namespace;
            result.Category = c.Category;

            return result;
        }

        protected  ISystemConfig<ICurrencySystem> convertToCurrencyConfig(DynamicTableEntity e)
        {
            var c = convertToObject(e);
            var result = new SystemConfig<ICurrencySystem>();

            result.Assembly = c.Assembly;
            result.AssemblyQualifiedName = c.AssemblyQualifiedName;
            result.Category = c.Category;
            result.ClassConfigID = c.ClassConfigID;
            result.Classname = c.Classname;
            result.ClassType = Common.Enum.ClassType.NewInstance;
            result.ConstructorParameters = c.ConstructorParameters;
            result.Namespace = c.Namespace;
            result.Category = c.Category;

            return result;
        }


        protected override DynamicTableEntity convertToEntity(ISystemConfig o)
        {
            DynamicTableEntity e = new DynamicTableEntity();

            e.SetProperty<string>("ID", o.ClassConfigID);
            e.SetProperty<string>("Assembly", o.Assembly);
            e.SetProperty<string>("AssemblyQualifiedName", o.AssemblyQualifiedName);
            e.SetProperty<string>("ClassConfigID",o.ClassConfigID);
            e.SetProperty<string>("Classname", o.Classname);
            e.SetProperty<string>("Namespace", o.Namespace);
            e.SetProperty<string>("ParametersCSV", string.Join(",", o.ConstructorParameters));
            e.SetProperty<string>("Category", o.Category);

            return e;


        }

        protected override IEnumerable<ISystemConfig> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            List<ISystemConfig> result = new List<ISystemConfig>();

            foreach(var e in  list)
            {
                result.Add(convertToObject(e));
            }

            return result;
        }

        protected override ISystemConfig convertToObject(DynamicTableEntity e)
        {
            SystemConfig o = new SystemConfig();

            o.Assembly = e.GetPropertyValue<string>("Assembly", string.Empty);
            o.AssemblyQualifiedName = e.GetPropertyValue<string>("AssemblyQualifiedName", string.Empty);
            o.ClassConfigID = e.GetPropertyValue<string>("ClassConfigID", string.Empty);
            o.Classname = e.GetPropertyValue<string>("Classname", string.Empty);
            o.Category = e.GetPropertyValue<string>("Category", string.Empty);

            //parameters
            string[] parameterNames = new string[0];

            try
            {
                var p = e.GetPropertyValue<string>("ParametersCSV", string.Empty);
                parameterNames = p.Split(",");
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                parameterNames = new string[0];
            }

            o.ConstructorParameters = parameterNames;
            o.Namespace = e.GetPropertyValue<string>("Namespace", string.Empty);

            return o;
        }

        protected override void deleteEntry(DynamicTableEntity e)
        {
            var id = e.GetPropertyValue<string>("ID", string.Empty);

            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + id;
            _table.Delete<DynamicTableEntity>(e.PartitionKey, e.RowKey);
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, replace);

            e.PartitionKey = _defaultPartitionKey + ":category:" + e.GetPropertyValue<string>("Category", string.Empty);
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, replace);
        }

        



        #endregion helpers
    }
}
