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
    public class ExternalClassRepository : BowtieRepository<JB2.Bowtie.IExternalClass>, IExternalClassRepository
    {
        #region Constructors

        public ExternalClassRepository() : this(AzureStorage.ExternalClassTable,AzureStorage.GeneralBlob,AzureStorage.ExternalClassTable)
        {

        }

        public ExternalClassRepository(Common.Data.AzureTableRepository table, Common.Data.AzureBlobRepository blob, Common.Data.AzureTableRepository playerdata)
        {
            _table = table;
            _blob = blob;
            _defaultPartitionKey = "externalclass";
            _playerData = playerdata;
        }


        #endregion Constructors


        protected override DynamicTableEntity convertToEntity(IExternalClass o)
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

        protected override IEnumerable<IExternalClass> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            List<IExternalClass> result = new List<IExternalClass>();

            foreach(var e in  list)
            {
                result.Add(convertToObject(e));
            }

            return result;
        }

        protected override IExternalClass convertToObject(DynamicTableEntity e)
        {
            ExternalClass o = new ExternalClass();

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
        }
    }
}
