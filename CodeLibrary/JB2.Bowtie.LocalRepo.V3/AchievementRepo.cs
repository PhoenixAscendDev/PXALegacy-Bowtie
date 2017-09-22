using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;
using JB2.Common;

namespace JB2.Bowtie.Data.Local
{
    public class AchievementRepo : JB2.Common.Singleton<AchievementRepo>, IAchievementRepository
    {
        #region Fields

        protected Dictionary<string, IAchievement> _items;
        protected List<AchievementStepRule> _steprules;

        private Dictionary<string, AchievementData> _playerdata;

        private Dictionary<string, ulong> _playerdataKeys;
        
        protected IUnitOfWork _uofw;


        #endregion Fields


        #region Constructors

        public AchievementRepo() : this(JB2.Settings.Bowtie.UnitOfWork)
        {
            
        }

        public AchievementRepo(IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;


            _items = new Dictionary<string, IAchievement>();
            _steprules = new List<AchievementStepRule>();

            _playerdata = new Dictionary<string, AchievementData>();
            _playerdataKeys = new Dictionary<string, ulong>();

            //if (_apps == null)
            //{
            //    _apps = new Dictionary<string, IApplication>();
            //    var json = "[{\"Website\":\"http://linkfence.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a600dcba\",\"Name\":\"Link Fence\"},{\"Website\":\"http://fivetwo.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a4cc70f2\",\"Name\":\"FiveTwo\"},{\"Website\":\"http://bluffstreet.fun/thisthat\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"9F0199E\",\"Name\":\"ThisThat\"}]";
            //    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocalApplication>>(json);

            //    foreach (var a in list)
            //    {
            //        _apps.Add(a.ID, a);
            //    }

            //}

        }


        #endregion Constructors


        #region AchievementStep

        public IEnumerable<AchievementStepRule> GetStepsByAchievementID(string achievementID)
        {
            return _steprules.Where(x => x.AchievementID == achievementID);
        }

        public void Insert(AchievementStepRule rule)
        {
            _steprules.Add(rule);
        }

        public void Delete(AchievementStepRule rule)
        {
            _steprules.Remove(rule);
        }


        #endregion AchievementStep


        #region AchievementData

        public AchievementData GetDataByPlayer(string applicationID, string playerID)
        {
            string key = applicationID + ">*<" + playerID;
            AchievementData result = null;

            if (_playerdata.ContainsKey(key))
                result = _playerdata[key];
            else
                result = null;

            return result;
        }

        public ServiceResult InsertAchievementData(string applicationID, string playerID, AchievementData data)
        {
            try
            {
                if (!data.isValid)
                    throw new Exception("AchievementData is not valid");

                string key = applicationID + ">*<" + playerID;

                if (_playerdataKeys.ContainsKey(key))
                {
                    var playerDataID = _playerdataKeys[key];

                    var verify = data.AchievementDataID;

                    if (verify == playerDataID)
                    {
                        _playerdata[key] = data;
                    }
                    else
                    {
                        throw new Exception("AchievementData does not match the one saved in the records");
                    }

                }
                else
                {
                    _playerdata[key] = data;
                    _playerdataKeys[key] = data.AchievementDataID;
                }




                return true;
            }
            catch (Exception ex)
            {
                return new Common.ServiceResult(ex);
            }
        }


        #endregion AchievementData


        #region AchievementEntry

        public IEnumerable<IAchievementEntry> GetEntriesByPlayer(string applicationID, string playerID)
        {
            var achievements = GetByApplicationID(applicationID);
            var ds = GetDataByPlayer(applicationID, playerID);
            List<IAchievementEntry> list = new List<IAchievementEntry>();

            foreach(var a in achievements)
            {
                var pts = ds.GetPoints(a.StorageSlot);
                var status = ds.GetStatus(a.StorageSlot);
                var dt = (status != Enum.AchievementStatusType.NotAcheived) ?  ds.GetDateAchieved(a.StorageSlot) : DateTime.MinValue;
                var steps = ds.GetStepValue(a.StorageSlot);

                IAchievementEntry e = new AchievementEntry();
                e.AchievementID = a.ID;
                e.ApplicationID = applicationID;
                e.PlayerID = playerID;
                e.DateEarned = dt;
                e.PercentComplete = steps != 0 ? (a.StepsRequired / steps) * 100 : 0;
                e.PointsEarned = pts;
                e.Status = status;

                list.Add(e);        
            }

            return list;
        }


        #endregion AchievementEntry

        public void Delete(IAchievement entity)
        {
            _items.Remove(entity.ID);
        }

        public IAchievement[] GetAll()
        {
            return GetAll(null);
        }

        public IAchievement[] GetAll(int? maxRecordCount)
        {
            if (maxRecordCount == null || maxRecordCount < 1)
                return getall().ToArray();
            else

            return getall().Take( (int)maxRecordCount).ToArray();

        }

        public IEnumerable<IAchievement> GetByApplicationID(string applicationID)
        {
            return getall().Where(x => x.ApplicationID == applicationID);
        }

        public IAchievement GetById(string id)
        {
            return getall().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(IAchievement entity)
        {
            
            _items.Add(entity.ID, entity);
        }

        public IAchievement[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IAchievement[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }



        #region Helpers

        public IEnumerable<IAchievement> getall()
        {

            return _items.Values.ToList();

            //var json = "[{ \"Website\":\"http://linkfence.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{ \"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a600dcba\",\"Name\":\"Link Fence\"},{ \"Website\":\"http://fivetwo.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{ \"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a4cc70f2\",\"Name\":\"FiveTwo\"},null]";

            //return Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocalApplication>>(json);


        }









        #endregion Helpers
    }
}
