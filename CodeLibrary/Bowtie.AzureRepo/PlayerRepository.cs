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
        public PlayerRepository() : this("players", "general")
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


        public override IBowtiePlayer GetById(string id)
        {
            var e = _table.GetEntity<DynamicTableEntity>("player", "id:" + id);
            if (e != null)
            {
                return convertToObject(e);
            }
            else
                return null;
        }

        public BowtieMetadata GetMetaDataByPlayerID(string playerID)
        {
            throw new NotImplementedException();
        }


        public void Insert(ApplicationPlayer player)
        {
            saveAppUser(convertToEntity(player), true);

            InsertAuthInfo(player.GetPlayerID(), player.AuthInfo);
        }

        public override void Insert(IBowtiePlayer obj)
        {
            base.Insert(obj);

            InsertAuthInfo(obj.GetPlayerID(), obj.GetAuthInfo());
        }

        public ApplicationPlayer GetAppPlayerByID(string playerID, string appID)
        {
            var e = _table.GetEntity<DynamicTableEntity>("applicationplayer:" + appID, "id:" + playerID);

            //e.PartitionKey = "applicationplayer:" + e.Properties["ApplicationID"];
            //e.RowKey = "id:" + e.Properties["PlayerID"].StringValue;

            if (e != null)
                return convertToAppPlayer(e);
            else
                return null;
            //throw new NotImplementedException();
        }

        public IBowtiePlayer GetPlayerByAuth(string authID, string provider)
        {
            var e = _table.GetEntity<DynamicTableEntity>("auth:" + provider, "authid:" + authID);            
            if (e != null)
            {
                var playerID = e.GetPropertyValue<string>("PlayerID", string.Empty);
                return this.GetById(playerID);
            }
              
            else
                return null;
        }


        #region AuthInfo

        public ServiceResult InsertAuthInfo(string playerID, AuthInfo authinfo)
        {
            DynamicTableEntity e = new DynamicTableEntity();
            e.SetProperty<string>("AuthProvider", authinfo.Provider);
            e.SetProperty<string>("UserID", authinfo.UserID);
            e.SetProperty<string>("PlayerID", playerID);

            e.PartitionKey = "auth:" + authinfo.Provider;
            e.RowKey = "authid:" + authinfo.UserID;

            _table.Insert<DynamicTableEntity>(e, true);

            return true;
        }

        public ServiceResult RemoveAuthInfo(string playerID, string authProvider)
        {
            throw new NotImplementedException();
        }




        #endregion AuthInfo

        #region Non-Public Methods

        protected override DynamicTableEntity convertToEntity(IBowtiePlayer o)
        {
            var e = new DynamicTableEntity();

            e.Properties.Add("ID", new EntityProperty(o.GetID()));
            e.Properties.Add("DisplayName", new EntityProperty(o.DisplayName));
            e.Properties.Add("Age", new EntityProperty(o.Age));
            e.Properties.Add("Gender", new EntityProperty(o.Gender));
            e.Properties.Add("PlayerID", new EntityProperty(o.GetPlayerID()));
            //e.Properties.Add("AuthProvider", new EntityProperty(o.AuthProvider));

            return e;
        }

        protected  DynamicTableEntity convertToEntity(ApplicationPlayer p)
        {
            var e = convertToEntity((IBowtiePlayer)p);

            e.Properties.Add("ApplicationID", new EntityProperty(p.GetApplicationID()));

            return e;
        }

        protected override IEnumerable<IBowtiePlayer> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            List<IBowtiePlayer> players = new List<IBowtiePlayer>(list.Count());

            foreach (var e in list)
            {
                players.Add(convertToObject(e));
            }

            return players;
        }

        protected override IBowtiePlayer convertToObject(DynamicTableEntity e)
        {
            BowtiePlayer player = new BowtiePlayer();

            player.ID = e.Properties.ContainsKey("ID") ? e.Properties["ID"].StringValue : string.Empty;
            player.DisplayName = e.Properties.ContainsKey("DisplayName") ? e.Properties["DisplayName"].StringValue : string.Empty;
            player.Age = e.Properties.ContainsKey("Age") ? e.Properties["Age"].Int32Value.GetValueOrDefault() : 0;
            player.Gender = e.Properties.ContainsKey("Gender") ? e.Properties["Gender"].StringValue : string.Empty;
            //player.AuthProvider = e.Properties.ContainsKey("AuthProvider") ? e.Properties["AuthProvider"].StringValue : string.Empty;

            return player;
        }

        protected  ApplicationPlayer convertToAppPlayer(DynamicTableEntity e)
        {
            
            var id = e.Properties.ContainsKey("ID") ? e.Properties["ID"].StringValue : string.Empty;
            var applicationid = e.Properties.ContainsKey("AppllicationID") ? e.Properties["AppllicationID"].StringValue : string.Empty;


            ApplicationPlayer player = new ApplicationPlayer(id, applicationid);
            player.ID = e.Properties.ContainsKey("ID") ? e.Properties["ID"].StringValue : string.Empty;
            player.DisplayName = e.Properties.ContainsKey("DisplayName") ? e.Properties["DisplayName"].StringValue : string.Empty;
            player.Age = e.Properties.ContainsKey("Age") ? e.Properties["Age"].Int32Value.GetValueOrDefault() : 0;
            player.Gender = e.Properties.ContainsKey("Gender") ? e.Properties["Gender"].StringValue : string.Empty;
            //player.AuthProvider = e.Properties.ContainsKey("AuthProvider") ? e.Properties["AuthProvider"].StringValue : string.Empty;
            player.DateRegistered = e.Properties.ContainsKey("DateRegistered") ? e.Properties["DateRegistered"].DateTime.GetValueOrDefault() : DateTime.MinValue;

            return player;

        }


        protected IPerson<string> convertToPerson(DynamicTableEntity e)
        {
            var person = new Person();
            person.ID = e.Properties.ContainsKey("PlayerID") ? e.Properties["PlayerID"].StringValue : string.Empty;
            person.DisplayName = e.Properties.ContainsKey("DisplayName") ? e.Properties["DisplayName"].StringValue : string.Empty;

            person.Name = new Name();

            return person;
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            e.PartitionKey = "profile";
            e.RowKey = "id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, true);

            e.PartitionKey = "player";
            e.RowKey = "id:" + e.Properties["PlayerID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, true);
        }

        protected void saveAppUser(DynamicTableEntity e, bool replace)
        {
            e.PartitionKey = "applicationplayer:" + e.Properties["ApplicationID"].StringValue;
            e.RowKey = "id:" + e.Properties["PlayerID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, true);

            saveEntity(e, replace);
        }



        #endregion Non-Public Methods

    }
}
