using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Table;

using JB2.Common;
using JB2.Common.Data;
using JB2.Identity;

using JB2.Bowtie;

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

        public AchievementRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob)
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
            //e.PartitionKey = _defaultPartitionKey + "player" + ":" + e.Properties["PlayerID"].StringValue;
            //e.RowKey = "app:" + achievement.ApplicationID + "_achievementid:" + achievement.GetID() + "_id:" + e.Properties["ID"].StringValue;
            //_table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

            var elist = _table.GetByRowKeyStartWith<DynamicTableEntity>(_defaultPartitionKey + "player" + ":" + playerID, "app:" + appID, 1000);

            return convertToPlayerAchievement(elist);
        }

        public IPlayerAchievement GetPlayerAchievement(string playerID, string achievementID)
        {
            var e = _table.GetEntity<DynamicTableEntity>("player:" + playerID, "achievementid:" + achievementID);

            if (e != null)
                return convertToPlayerAchievement(e);
            else
            {
                return null;
            }
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
            e.Properties.Add("DateAchieved", new EntityProperty(o.DateAchieved.ToString() ));

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
            e.Properties.Add("Rarity", new EntityProperty(o.Rarity.ToString()));
            e.Properties.Add("StepFx", new EntityProperty(o.StepFx));
            e.Properties.Add("StepRequired", new EntityProperty(o.StepsRequired));
            e.Properties.Add("TimeBoundEnd", new EntityProperty(o.TimeBoundEnd.ToString()));
            e.Properties.Add("TimeBoundStart", new EntityProperty(o.TimeBoundStart.ToString()));
            e.Properties.Add("UniqueToken", new EntityProperty(o.UniqueToken));
            e.Properties.Add("SortOrder", new EntityProperty(o.SortOrder));
            e.Properties.Add("StepType", new EntityProperty(o.StepType.ToString()));

            List<string> points = new List<string>(o.PointSystems);

            foreach (var p in o.PointSystems)
            {
                string point = p + ":" + o.GetPoints(p).ToString();
                points.Add(point);
            }

            e.SetProperty<string>("Points", string.Join(",", points.ToArray()));

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

        protected IPlayerAchievement[] convertToPlayerAchievement(IEnumerable<DynamicTableEntity> elist)
        {
            var result = new List<IPlayerAchievement>(elist.Count());
            foreach(var e in elist)
            {
                result.Add(convertToPlayerAchievement(e));
            }

            return result.ToArray();
        }
        protected IPlayerAchievement convertToPlayerAchievement(DynamicTableEntity e)
        {

            var id = e.Properties["ID"].StringValue;
            IPlayerAchievement result = new PlayerAchievement(id);

            result.AchievementID = e.Properties.ContainsKey("AchievementID") ? e.Properties["AchievementID"].StringValue : string.Empty;
            result.CurrentStep = e.Properties.ContainsKey("CurrentStep") ? e.Properties["CurrentStep"].Int32Value.GetValueOrDefault() : 0;
            result.ID = e.Properties.ContainsKey("ID") ? e.Properties["ID"].StringValue : string.Empty;
            result.Name = e.Properties.ContainsKey("Name") ? e.Properties["Name"].StringValue : string.Empty;
            result.PlayerID = e.Properties.ContainsKey("PlayerID") ? e.Properties["PlayerID"].StringValue : string.Empty;
            result.PointsEarned = e.Properties.ContainsKey("PointsEarned") ? e.Properties["PointsEarned"].Int32Value.GetValueOrDefault() : 0;
            result.DateAchieved = e.Properties.ContainsKey("DateAchieved") ? Convert.ToDateTime(e.Properties["DateAchieved"].StringValue) : DateTime.MinValue;
            
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

            result.Name = e.Properties.ContainsKey("Name") ? e.Properties["Name"].StringValue : string.Empty;
            result.AchievementType = type;
            result.ApplicationID = e.Properties.ContainsKey("ApplicationID") ? e.Properties["ApplicationID"].StringValue : string.Empty;
            result.Category = e.Properties.ContainsKey("Category") ? e.Properties["Category"].StringValue : string.Empty;
            result.Description = e.Properties.ContainsKey("Description") ? e.Properties["Description"].StringValue : string.Empty;

            var drewdroptriggers = e.Properties.ContainsKey("DewdropCSV") ? e.Properties["DewdropCSV"].StringValue.Split(',') : new string[0];
            result.DewdropTriggers = drewdroptriggers;

            result.EarnedIconUrl = e.Properties.ContainsKey("EarnedIconUrl") ? e.Properties["EarnedIconUrl"].StringValue : string.Empty;
            result.HiddenIconUrl = e.Properties.ContainsKey("HiddenIconUrl") ? e.Properties["HiddenIconUrl"].StringValue : string.Empty;
            result.ShownIconUrl = e.Properties.ContainsKey("ShownIconUrl") ? e.Properties["ShownIconUrl"].StringValue : string.Empty;
           

            result.Rarity = e.Properties.ContainsKey("Rarity") ? (Enum.AchievementRarityType)System.Enum.Parse(typeof(Enum.AchievementRarityType), e.Properties["Rarity"].StringValue) : Enum.AchievementRarityType.Common;

            result.StepFx = e.Properties.ContainsKey("StepRegEx") ? e.Properties["StepRegEx"].StringValue : string.Empty;
            result.StepsRequired = e.Properties.ContainsKey("StepRequired") ? e.Properties["StepRequired"].Int32Value.GetValueOrDefault() : 0;
            result.StepType = e.Properties.ContainsKey("StepType") ? (Enum.StepFxType)System.Enum.Parse(typeof(Enum.StepFxType), e.Properties["StepType"].StringValue) : Enum.StepFxType.Empty;

            result.TimeBoundEnd = e.Properties.ContainsKey("TimeBoundEnd") ? Convert.ToDateTime(e.Properties["TimeBoundEnd"].StringValue) : System.DateTime.MinValue;
            result.TimeBoundStart = e.Properties.ContainsKey("TimeBoundStart") ? Convert.ToDateTime(e.Properties["TimeBoundStart"].StringValue) : System.DateTime.MinValue;

            //load the points per each point system
            Dictionary<string, int> pointsDic = new Dictionary<string, int>();
            string points = e.GetPropertyValue<string>("Points",string.Empty);
            foreach (var p in points.Split(","))
            {
                try
                {
                    var systemid = p.Split(":")[0];
                    var point = p.Split(":")[1];
                    int pointint = 0;
                    int.TryParse(point, out pointint);
                    pointsDic.Add(systemid, pointint);
                }
                catch(Exception ex)
                {
                    ex.BowtieLog();
                }
            }
            ((Achievement)result).Points = pointsDic;

            //result.UniqueToken = e.Properties["UniqueToken"].StringValue;



            return result;
        }

        protected override void deleteEntry(DynamicTableEntity e)
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

            e.PartitionKey = _defaultPartitionKey + "player:" + e.Properties["PlayerID"].StringValue;
            e.RowKey = "achievementid:" + achievement.GetID();
            _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);

        }
    }
}
