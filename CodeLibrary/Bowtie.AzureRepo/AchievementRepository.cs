using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Table;

using JB2.Common;
using JB2.Common.Data;
using JB2.Identity;

namespace JB2.Bowtie.Data.Azure
{
    public class AchievementRepository : BowtieRepository<IAchievement>, IAchievementRepository
    {
        #region Constructors
        public AchievementRepository() : this("achievements","general")
        {

        }

        protected AchievementRepository(string tableName, string blobName)
            : this(JB2.Infrastructure.Storage.BowtieAccount.GetTable(tableName),
                   JB2.Infrastructure.Storage.BowtieAccount.GetBlog(blobName))
        {

        }

        public AchievementRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob) : base()
        {
            _table = azureTable;
            _blob = azureBlob;
            _defaultPartitionKey = "achievement";
        }

        #endregion

        public IAchievement[] GetAchievementsByApplication(string appID)
        {
            var elist = _table.GetByRowKeyStartWith<DynamicTableEntity>(_defaultPartitionKey + ":app:" + appID, "id:",1000);

            return convertToObject(elist).ToArray();
           
        }

        public IPlayerAchievement[] GetPlayerAchievements(string playerID, string appID)
        {
            throw new NotImplementedException();
        }

        public void Insert(IPlayerAchievement playerAchievement)
        {
            savePlayerAchievement(convertToEntity(playerAchievement), true);

            
        }

        public override IAchievement[] SearchFor(bool useCache = true)
        {
            throw new NotImplementedException();
        }

        public override IAchievement[] SearchFor(string filter, bool useCache = true)
        {
            throw new NotImplementedException();
        }

        protected  DynamicTableEntity convertToEntity(IPlayerAchievement o)
        {
            var e = new DynamicTableEntity();
            e.Properties.Add("AchievementID", new EntityProperty(o.AchievementID));
            e.Properties.Add("CurrentStep", new EntityProperty(o.CurrentStep));
            e.Properties.Add("PlayerID", new EntityProperty(o.PlayerID));
            e.Properties.Add("PointsEarned", new EntityProperty(o.PointsEarned));
            e.Properties.Add("ID", new EntityProperty(o.GetID()));
            e.Properties.Add("Name", new EntityProperty(o.GetName()));
            e.Properties.Add("Kind", new EntityProperty(o.GetKind().ToString()));
            e.Properties.Add("UniqueToken", new EntityProperty(o.UniqueToken));
            e.Properties.Add("LastUpdate", new EntityProperty(o.GetLastUpdate()));

            return e;
        }

        protected override DynamicTableEntity convertToEntity(IAchievement o)
        {
            var e = new DynamicTableEntity();

            e.Properties.Add("ID", new EntityProperty(o.GetID()));
            e.Properties.Add("Kind", new EntityProperty(o.GetKind().ToString()));
            e.Properties.Add("LastUpdate", new EntityProperty(o.GetLastUpdate()));
            e.Properties.Add("Name", new EntityProperty(o.GetName()));
            e.Properties.Add("AchievementType", new EntityProperty(o.AchievementType.ToString()));
            e.Properties.Add("ApplicationID", new EntityProperty(o.ApplicationID));
            e.Properties.Add("Category", new EntityProperty(o.Category));
            e.Properties.Add("Description", new EntityProperty(o.Description));
            e.Properties.Add("DewdropCSV", new EntityProperty(string.Join(",", o.DewdropTriggers.ToArray())));
            e.Properties.Add("EarnedIconUrl", new EntityProperty(o.EarnedIconUrl));
            e.Properties.Add("HiddenIconUrl", new EntityProperty(o.HiddenIconUrl));
            e.Properties.Add("ShownIconUrl", new EntityProperty(o.ShownIconUrl));
            e.Properties.Add("Points", new EntityProperty(o.Points));
            e.Properties.Add("Rarity", new EntityProperty(o.Rarity.ToString()));
            e.Properties.Add("StepRegEx", new EntityProperty(o.StepRegex));
            e.Properties.Add("StepRequired", new EntityProperty(o.StepsRequired));
            e.Properties.Add("TimeBoundEnd", new EntityProperty(o.TimeBoundEnd));
            e.Properties.Add("TimeBoundStart", new EntityProperty(o.TimeBoundStart));
            e.Properties.Add("UniqueToken", new EntityProperty(o.UniqueToken));
            e.Properties.Add("SortOrder", new EntityProperty(o.SortOrder));

            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + o.GetID();

            return e;       
        }




        protected override IEnumerable<IAchievement> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            var result = new List<IAchievement>();

            foreach(var e in list)
            {
                result.Add(convertToObject(e));
            }

            return result;
        }


        protected IPlayerAchievement convertToPlayerAchievement(DynamicTableEntity e)
        {

            var id = e.Properties["ID"].StringValue;
            IPlayerAchievement result = new PlayerAchievement(id);

            result.AchievementID = e.Properties["AchievementID"].StringValue;
            result.CurrentStep = e.Properties["CurrentStep"].Int32Value.GetValueOrDefault();
            result.ID = e.Properties["ID"].StringValue;
            result.Name = e.Properties["Name"].StringValue;
            result.PlayerID = e.Properties["PlayerID"].StringValue;
            result.PointsEarned = e.Properties["PointsEarned"].Int32Value.GetValueOrDefault();
            
            //var e = new DynamicTableEntity();
            //e.Properties.Add("AchievementID", new EntityProperty(o.AchievementID));
            //e.Properties.Add("CurrentStep", new EntityProperty(o.CurrentStep)));
            //e.Properties.Add("PlayerID", new EntityProperty(o.PlayerID));
            //e.Properties.Add("PointsEarned", new EntityProperty(o.PointsEarned));
            //e.Properties.Add("ID", new EntityProperty(o.GetID()));
            //e.Properties.Add("LastUpdate", new EntityProperty(o.GetLastUpdate()));

            return result;

        }
        protected override IAchievement convertToObject(DynamicTableEntity e)
        {
            IAchievement result = null;

            Enum.AchievementType type = e.Properties.ContainsKey("AchievementType") ? (Enum.AchievementType)System.Enum.Parse(typeof(Enum.AchievementType), e.Properties["AchievementType"].StringValue) : Enum.AchievementType.Standard;
            string id = e.Properties.ContainsKey("ID") ? e.Properties["ID"].StringValue : string.Empty;

            switch (type)
            {
                case Enum.AchievementType.TimeBound:
                    result = new EventAchievement(id);
                    break;
                default :
                    result = new StandardAchievement(id);
                    break;
            }

            result.Name = e.Properties["Name"].StringValue;
            result.AchievementType = type;
            result.ApplicationID = e.Properties["ApplicationID"].StringValue;
            result.Category = e.Properties["Category"].StringValue;
            result.Description = e.Properties["Description"].StringValue;

            var drewdroptriggers = e.Properties["DewdropCSV"].StringValue.Split(',');
            result.DewdropTriggers = drewdroptriggers;

            result.EarnedIconUrl = e.Properties["EarnedIconUrl"].StringValue;
            result.HiddenIconUrl = e.Properties["HiddenIconUrl"].StringValue;
            result.ShownIconUrl = e.Properties["ShownIconUrl"].StringValue;
            result.Points = (long)e.Properties["Points"].Int64Value;

            result.Rarity = e.Properties.ContainsKey("Rarity") ? (Enum.AchievementRarityType)System.Enum.Parse(typeof(Enum.AchievementRarityType), e.Properties["Rarity"].StringValue) : Enum.AchievementRarityType.Common;

            result.StepRegex = e.Properties["StepRegEx"].StringValue;
            result.StepsRequired = e.Properties["StepRequired"].Int32Value.GetValueOrDefault();

            result.TimeBoundEnd = e.Properties["TimeBoundEnd"].DateTime.GetValueOrDefault();
            result.TimeBoundStart = e.Properties["TimeBoundStart"].DateTime.GetValueOrDefault();

            //result.UniqueToken = e.Properties["UniqueToken"].StringValue;

            

            return result;
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            //first delete any records associated with partiion of the id
            e.PartitionKey = _defaultPartitionKey + ":" + e.Properties["ID"].StringValue;
            var marktoDelete = _table.GetByPartitionKey<DynamicTableEntity>(e.PartitionKey, 1000);
            foreach (var d in marktoDelete)
            {
                _table.Delete<DynamicTableEntity>(d.PartitionKey, d.RowKey);
            }



            //standard by ID
            e.PartitionKey = _defaultPartitionKey;
            e.RowKey = "id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

            //by Application
            e.PartitionKey = _defaultPartitionKey + ":app:" + e.Properties["ApplicationID"].StringValue;
            e.RowKey = "id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

            e.PartitionKey = _defaultPartitionKey + ":type:" + e.Properties["AchievementType"].StringValue;
            e.RowKey = "id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

            //by dewdrops
            List<string> dewdrops = e.Properties["DewdropCSV"].StringValue.Split(',').ToList();
            e.PartitionKey = _defaultPartitionKey + ":" + e.Properties["ID"].StringValue;
            foreach( string dewdrop in dewdrops)
            {
                e.RowKey  = "dewdrop:" + dewdrop + "_id:" + e.Properties["ID"].StringValue;
                _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);
            }
        }

        protected  void savePlayerAchievement(DynamicTableEntity e, bool replace)
        {
            var achievement = this.GetById(e.Properties["AchievementID"].StringValue);
            //standard by ID
            e.PartitionKey = _defaultPartitionKey + "player";
            e.RowKey = "id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

            //by player and app
            e.PartitionKey = _defaultPartitionKey + "player" + ":" + e.Properties["PlayerID"].StringValue;
            e.RowKey = "app:" + achievement.ApplicationID + "_achievementid:" + achievement.GetID() +  "_id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

            e.PartitionKey = _defaultPartitionKey + "player";
            e.RowKey = "achievementid:" + achievement.GetID() + "_playerID:" + e.Properties["PlayerID"].StringValue + "_id:"  + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

        }
    }
}
