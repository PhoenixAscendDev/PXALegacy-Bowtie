using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Data.Local
{
    public class AchievementRepo : JB2.Common.Singleton<AchievementRepo>, IAchievementRepository
    {
        #region Fields

        protected Dictionary<string, IAchievement> _items;
        protected List<AchievementStepRule> _steprules;
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
