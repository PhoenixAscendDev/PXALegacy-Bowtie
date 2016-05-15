using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using JB2.Identity;


using Microsoft.WindowsAzure.Storage.Table;
using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public class PlayerRepository : BowtieRepository<IBowtiePlayer>, IBowtiePlayerRespository
    {
        #region Constructors
        public PlayerRepository() : this("players","general")
        {

        }

        protected PlayerRepository(string tableName, string blobName)
            : this(JB2.Infrastructure.Storage.BowtieAccount.GetTable(tableName),
                   JB2.Infrastructure.Storage.BowtieAccount.GetBlog(blobName))
        {

        }

        public PlayerRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob) : base()
        {
            _table = azureTable;
            _blob = azureBlob;
            _defaultPartitionKey = "player";
        }

        #endregion

        public BowtieMetadata GetMetaDataByPlayerID(string playerID)
        {
            throw new NotImplementedException();
        }

        protected override DynamicTableEntity convertToEntity(IBowtiePlayer o)
        {
            var e = new DynamicTableEntity();

            e.Properties.Add("ID", new EntityProperty(o.GetID()));
            e.Properties.Add("DisplayName", new EntityProperty(o.DisplayName));
            e.Properties.Add("Age", new EntityProperty(o.GetDefaultProfile().Age));
            e.Properties.Add("Gender", new EntityProperty(o.GetDefaultProfile().Gender));

            return e;
        }

        protected override IEnumerable<IBowtiePlayer> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            throw new NotImplementedException();
        }

        protected override IBowtiePlayer convertToObject(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            e.PartitionKey = "player";
            e.RowKey = "id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, true);
        }
    }
}
