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
    public class MissionRespository : BowtieRepository<IMission>, IMissionRespository
    {

        #region Methods
        public IEnumerable<IMission> GetMissionsByGroupID(string id)
        {
            throw new NotImplementedException();
        }

        protected override DynamicTableEntity convertToEntity(IMission o)
        {
            var e = new DynamicTableEntity();



            e.SetProperty<string>("ID",o.GetID());
            e.SetProperty<string>("Kind",o.GetKind().ToString());
            e.SetProperty<DateTime>("LastUpdate", o.GetLastUpdate());
            e.SetProperty<string>("Name",o.GetName());
            e.SetProperty<string>("AchievementType", o.AchievementType.ToString());
            e.SetProperty<string>("ApplicationID", o.ApplicationID);
            e.SetProperty<string>("Category",o.Category);
            e.SetProperty<string>("Description", o.Description);
            e.SetProperty<string>("DewdropCSV", string.Join(",", o.DewdropTriggers.ToArray()));
            e.SetProperty<string>("EarnedIconUrl", o.EarnedIconUrl);
            e.SetProperty<string>("HiddenIconUrl", o.HiddenIconUrl);
            e.SetProperty<string>("ShownIconUrl", o.ShownIconUrl);
            e.SetProperty<string>("Rarity", o.Rarity.ToString());
            e.SetProperty<string>("StepFx", o.StepFx);
            e.SetProperty<int>("StepRequired", o.StepsRequired);
            e.SetProperty<string>("TimeBoundEnd", o.TimeBoundEnd.ToString());
            e.SetProperty<string>("TimeBoundStart", o.TimeBoundStart.ToString());
            e.SetProperty<string>("UniqueToken", o.UniqueToken);
            e.SetProperty<int>("SortOrder", o.SortOrder);
            e.SetProperty<string>("StepType",o.StepType.ToString());

            e.SetProperty<DateTime>("ExpireDate", o.ExpireDate);
            e.SetProperty<string>("NextMissionID", o.NextMissionID);
            e.SetProperty<string>("PreviousMissionID", o.PreviousMissionID);
            e.SetProperty<string>("MissionGroup", o.MissionGroup);

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

        protected override IEnumerable<IMission> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            var result = new List<IMission>();

            foreach (var e in list)
            {
                result.Add(convertToObject(e));
            }

            return result;

            //result.UniqueToken = e.Properties["UniqueToken"].StringValue;
        }

        protected override IMission convertToObject(DynamicTableEntity e)
        {
            IMission result = null;

            Enum.AchievementType type = e.Properties.ContainsKey("AchievementType") ? (Enum.AchievementType)System.Enum.Parse(typeof(Enum.AchievementType), e.Properties["AchievementType"].StringValue) : Enum.AchievementType.Standard;
            string id = e.Properties.ContainsKey("ID") ? e.Properties["ID"].StringValue : string.Empty;

            switch (type)
            {
                case Enum.AchievementType.Task:
                default:
                    result = new Mission(id);
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


            result.ExpireDate = e.GetPropertyValue<DateTime>("ExpireDate", DateTime.MaxValue);
            result.NextMissionID = e.GetPropertyValue<string>("NextMissionID", string.Empty);
            result.PreviousMissionID = e.GetPropertyValue<string>("PreviousMissionID", string.Empty);
            result.MissionGroup = e.GetPropertyValue<string>("MissionGroup", string.Empty);

            
            //load the points per each point system
            Dictionary<string, int> pointsDic = new Dictionary<string, int>();
            string points = e.GetPropertyValue<string>("Points", string.Empty);
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
                catch (Exception ex)
                {
                    ex.BowtieLog();
                }
            }
            ((Mission)result).Points = pointsDic;


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
            foreach (string dewdrop in dewdrops)
            {
                e.RowKey = "dewdrop:" + dewdrop + "_id:" + e.Properties["ID"].StringValue;
                _table.Insert<DynamicTableEntity>(e, TableInsertMode.Merge, false);
            }
        }


        #endregion Methods
    }
}
