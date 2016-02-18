using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public class DewdropRepository : BaseRepository<Dewdrop,DewdropEntity>,  IDewdropRepository
    {
  
        #region Constructors
        public DewdropRepository() : this("dewdrops","general")
        {
            
        }

        protected DewdropRepository(string tableName, string blobName)
            : this(JB2.Infrastructure.Storage.BowtieAccount.GetTable(tableName),
                   JB2.Infrastructure.Storage.BowtieAccount.GetBlog(blobName))
        {

        }

        public DewdropRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob) : base()
        {
            _table = azureTable;
            _blob = azureBlob;
            _defaultPartitionKey = "dewdrop";
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
            return e;
        }


        public IEnumerable<IPlayerDewdrop> GetPlayerDewsByPlayerID(string playerID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPlayerDewdrop> GetPlayerDews(string playerID, string dewdropID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPlayerDewdrop> GetPlayerDewsBySearch(object search)
        {
            throw new NotImplementedException();
        }

        protected override Dewdrop convertToObject(DewdropEntity e)
        {
            if (e == null)
                throw new NullReferenceException();
            var result = new Dewdrop(e.ID, e.Name,e.Description,e.ApplicationID, e.GraphID);
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

        public void SavePlayerDew(IPlayerDewdrop playerdew)
        {
            throw new NotImplementedException();
        }
    }
}
