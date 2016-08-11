using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using Microsoft.WindowsAzure.Storage.Table;

using JB2.Bowtie;
using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public class GameCommandRepository : BowtieRepository<IGameCommand>, IGameCommandRepository
    {
        #region Constructors
        public GameCommandRepository() : this("gamecommands","general")
        {

        }

        protected GameCommandRepository(string tableName, string blobName)
            : this(JB2.Infrastructure.Storage.BowtieAccount.GetTable(tableName),
                   JB2.Infrastructure.Storage.BowtieAccount.GetBlog(blobName))
        {

        }

        public GameCommandRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob)
        {
            _table = azureTable;
            _blob = azureBlob;
            _defaultPartitionKey = "gamecommand";
        }

        #endregion

        public IGameCommand[] GetByGameID(string gameID)
        {
            var eList = _table.GetByRowKeyStartWith<DynamicTableEntity>(_defaultPartitionKey + ":game:" + gameID, "id:",1000);

            return convertToObject(eList).ToArray();

        }

        protected override DynamicTableEntity convertToEntity(IGameCommand o)
        {
            DynamicTableEntity e = new DynamicTableEntity();

            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + o.GetID();
            e.Properties.Add("CommandCode", new EntityProperty(o.CommandCode));
            e.Properties.Add("ID", new EntityProperty(o.GetID()));
            e.Properties.Add("Name", new EntityProperty(o.GetName()));
            e.Properties.Add("IssuePlayerID", new EntityProperty(o.IssuedPlayerID));
            e.Properties.Add("AffectedPlayerID", new EntityProperty(o.AffectedPlayerID));
            e.Properties.Add("ApplicationID", new EntityProperty(o.AppID));
            e.Properties.Add("GameID", new EntityProperty(o.GameID));

            return e;

        }

        protected override IEnumerable<IGameCommand> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            List<IGameCommand> cs = new List<IGameCommand>();

            foreach(var e in list)
            {
                cs.Add(convertToObject(e));
            }

            return cs.ToArray();


        }

        protected override IGameCommand convertToObject(DynamicTableEntity e)
        {
            var id = e.GetPropertyValue<string>("ID", string.Empty);

            GameCommand gc = new GameCommand(id);

            gc.Name = e.GetPropertyValue<string>("Name", string.Empty);
            gc.IssuedPlayerID = e.GetPropertyValue<string>("IssuePlayerID", string.Empty);
            gc.AffectedPlayerID = e.GetPropertyValue<string>("AffectedPlayerID", string.Empty);
            gc.AppID = e.GetPropertyValue<string>("ApplicationID", string.Empty);
            gc.GameID = e.GetPropertyValue<string>("GameID", string.Empty);
            gc.CommandCode = e.GetPropertyValue<string>("CommandCode", string.Empty);
            //e.PartitionKey = "gamecommand";
            //e.RowKey = "id:" + o.GetID();
            //e.Properties.Add("CommandCode", new EntityProperty(o.CommandCode));
            //e.Properties.Add("ID", new EntityProperty(o.GetID()));
            //e.Properties.Add("Name", new EntityProperty(o.GetName()));
            //e.Properties.Add("IssuePlayerID", new EntityProperty(o.IssuedPlayerID));
            //e.Properties.Add("AffectedPlayerID", new EntityProperty(o.AffectedPlayerID));
            //e.Properties.Add("ApplicationID", new EntityProperty(o.AppID));
            //e.Properties.Add("GameID", new EntityProperty(o.GameID));

            return gc;
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {

            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, true);

            //by application
            e.PartitionKey = _defaultPartitionKey + ":app:" + e.GetPropertyValue<string>("Application", string.Empty);
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, true);

            e.PartitionKey = _defaultPartitionKey + ":app:" + e.GetPropertyValue<string>("Application", string.Empty);
            e.RowKey = "gameid:" + e.GetPropertyValue<string>("GameID", string.Empty) + "_id:" + e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, true);

            //by game
            e.PartitionKey = _defaultPartitionKey + ":game:" + e.GetPropertyValue<string>("GameID", string.Empty);
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, true);

            
        }
    }
}
