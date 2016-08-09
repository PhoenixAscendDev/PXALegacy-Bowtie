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
    public class DewdropRepository : BowtieRepository<Dewdrop,DewdropEntity>,  IDewdropRepository
    {

        #region Fields

        protected AzureTableRepository _playerTable;

        #endregion Fields

        #region Constructors
        public DewdropRepository() : this("dewdrops","general")
        {
            
        }

        protected DewdropRepository(string tableName, string blobName)
            : this(JB2.Infrastructure.Storage.BowtieAccount.GetTable(tableName),
                   JB2.Infrastructure.Storage.BowtieAccount.GetBlog(blobName),
                   JB2.Infrastructure.Storage.BowtieAccount.GetTable(tableName + "players"))
        {

        }

        public DewdropRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob, AzureTableRepository playerTable) : base()
        {
            _table = azureTable;
            _blob = azureBlob;
            _defaultPartitionKey = "dewdrop";

            _playerTable = playerTable;
        }

        #endregion Constructors

        public IEnumerable<Dewdrop> GetByApplicationID(string appID)
        {
            var list_e = _table.GetByRowKeyStartWith<DewdropEntity>("app:" + appID, "id:",1000);
            return convertToObject(list_e);
        }

        protected override DewdropEntity convertToEntity(Dewdrop o)
        {
            var e = new DewdropEntity(_defaultPartitionKey, "id:" + o.GetID());
            e.ApplicationID = o.GetApplicationID();
            e.GraphID = o.GetGraphID();
            e.ID = o.GetID();
            e.Name = o.GetName();
            e.Description = o.GetDescription();
            e.jBeanCost = o.GetjBeanCost();
            return e;
        }

        protected DynamicTableEntity convertToEntity(IPlayerDewdrop o)
        {
            var e = new DynamicTableEntity("log", "id:" + o.GetID());
            e.Properties.Add("ID", EntityProperty.GeneratePropertyForString(o.GetID()));
            e.Properties.Add("Name", EntityProperty.GeneratePropertyForString(o.GetName()));
            e.Properties.Add("ApplicationID", EntityProperty.GeneratePropertyForString(o.GetApplicationID()));
            e.Properties.Add("DewdropID", EntityProperty.GeneratePropertyForString(o.GetDewdropID()));
            e.Properties.Add("PlayerID", EntityProperty.GeneratePropertyForString(o.GetPlayerID()));
            e.Properties.Add("DewDate", EntityProperty.CreateEntityPropertyFromObject(o.GetDewDate()));
            e.Properties.Add("Value", EntityProperty.CreateEntityPropertyFromObject(o.GetValue()));
            return e;
        }


        public IEnumerable<IPlayerDewdrop> GetPlayerDewsByPlayerID(string playerID)
        {
            var elements = _playerTable.GetByRowKeyStartWith<DynamicTableEntity>("player:" + playerID, "dewlog:",1000);
            return convertToPlayerDewdrop(elements);
        }

        public IEnumerable<IPlayerDewdrop> GetPlayerDews(string playerID, string dewdropID)
        {
            var elements = _playerTable.GetByRowKeyStartWith<DynamicTableEntity>("player:" + playerID, "dewlog:" + dewdropID, 1000);
            return convertToPlayerDewdrop(elements);
        }

        public IEnumerable<IPlayerDewdrop> GetPlayerDewsBySearch(object search)
        {
            throw new NotImplementedException();
        }

        public void InsertPlayerDew(IPlayerDewdrop playerdew)
        {
            savePlayerDew(convertToEntity(playerdew), true);
        }

        protected override Dewdrop convertToObject(DewdropEntity e)
        {
            if (e == null)
                throw new NullReferenceException();
            var result = new Dewdrop(e.ID, e.Name,e.Description,e.ApplicationID, e.GraphID,e.jBeanCost);
           
            return result;
        }

        protected IPlayerDewdrop convertToPlayerDewdrop(DynamicTableEntity e)
        {
            List<IMetaData> metadata = new List<IMetaData>();

            metadata.Add(new MetaData<string>("ApplicationID", e.Properties["ApplicationID"].StringValue));
            metadata.Add(new MetaData<DateTime>("DewDate", (DateTime)e.Properties["DewDate"].DateTime));
            metadata.Add(new MetaData<string>("DewdropID", e.Properties["DewdropID"].StringValue));
            metadata.Add(new MetaData<string>("PlayerID", e.Properties["PlayerID"].StringValue));

            
            var result = new PlayerDewdrop(metadata, e.Properties["Value"].StringValue);
            result.ID = e.Properties["ID"].StringValue;
            result.Name = e.Properties["Name"].StringValue;

            return result;
        }

        protected override IEnumerable<Dewdrop> convertToObject(IEnumerable<DewdropEntity> list)
        {
            List<Dewdrop> result = new List<Dewdrop>(list.Count());

            foreach(DewdropEntity e in list)
            {
                    result.Add(convertToObject(e));
            }

            return result;
        }

        protected  IEnumerable<IPlayerDewdrop> convertToPlayerDewdrop(IEnumerable<DynamicTableEntity> list)
        {
            List<IPlayerDewdrop> result = new List<IPlayerDewdrop>(list.Count());

            foreach (DynamicTableEntity e in list)
            {
                result.Add(convertToPlayerDewdrop(e));
            }

            return result;
        }

        protected void savePlayerDew(DynamicTableEntity e, bool replace)
        {
            e.PartitionKey = "log";
            e.RowKey = "id:" + e.Properties["ID"].StringValue;
            _playerTable.Insert<DynamicTableEntity>(e, replace);

            e.PartitionKey = "app:" + e.Properties["ApplicationID"].StringValue;
            e.RowKey = "dewlog:" + e.Properties["DewdropID"].StringValue + ">*<" + e.Properties["ID"].StringValue;
            _playerTable.Insert<DynamicTableEntity>(e, replace);

            e.PartitionKey = "player";
            e.RowKey = "dewlog:" + e.Properties["DewdropID"].StringValue + ">*<" + e.Properties["ID"].StringValue;
            _playerTable.Insert<DynamicTableEntity>(e, replace);

            e.PartitionKey = "player:" + e.Properties["PlayerID"].StringValue;
            e.RowKey = "dewlog:" + e.Properties["DewdropID"].StringValue + ">*<" + e.Properties["ID"].StringValue;
            _playerTable.Insert<DynamicTableEntity>(e, replace);

        }

        protected override void saveEntity(DewdropEntity e, bool replace)
        {
            //default partition
            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + e.GetID();
            _table.Insert<DewdropEntity>(e, replace);

            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "graph:" + e.GraphID + ">*<" + e.GetID();
            _table.Insert<DewdropEntity>(e, replace);

            //application partition
            e.PartitionKey = "app:" + e.ApplicationID;
            e.RowKey = "id:" + e.GetID();
            _table.Insert<DewdropEntity>(e, replace);


            e.PartitionKey = "app:" + e.ApplicationID;
            e.RowKey = "graph:" + e.GraphID + ">*<" + e.GetID();
            _table.Insert<DewdropEntity>(e, replace);



        }

        protected override void deleteAll(DewdropEntity e)
        {
            //default partition
            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + e.GetID();
            _table.Delete<DewdropEntity>(e.PartitionKey, e.RowKey);

            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "graph:" + e.GraphID + ">*<" + e.GetID();
            _table.Delete<DewdropEntity>(e.PartitionKey, e.RowKey);

            //application partition
            e.PartitionKey = "app:" + e.ApplicationID;
            e.RowKey = "id:" + e.GetID();
            _table.Delete<DewdropEntity>(e.PartitionKey, e.RowKey);

            e.PartitionKey = "app:" + e.ApplicationID;
            e.RowKey = "graph:" + e.GraphID + ">*<" + e.GetID();
            _table.Delete<DewdropEntity>(e.PartitionKey, e.RowKey);
        }

       
    }
}
