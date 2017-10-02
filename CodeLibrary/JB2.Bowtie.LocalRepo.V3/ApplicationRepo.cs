using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Local
{
    public class ApplicationRepo : JB2.Common.Singleton<ApplicationRepo>, IApplicationRepository
    {

        #region Fields

        protected Dictionary<string, IApplication> _apps;
        protected IUnitOfWork _uofw;

        #endregion Fields


        #region Constructors

        public ApplicationRepo() : this(JB2.Settings.Bowtie.UnitOfWork)
        {
            
        }

        public ApplicationRepo(IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;

            if(_apps == null)
            {
                _apps = new Dictionary<string, IApplication>();
                var json = "[{\"Website\":\"http://linkfence.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a600dcba\",\"Name\":\"Link Fence\"},{\"Website\":\"http://fivetwo.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a4cc70f2\",\"Name\":\"FiveTwo\"},{\"Website\":\"http://bluffstreet.fun/thisthat\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{\"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"9F0199E\",\"Name\":\"ThisThat\"}]";
                var list = JB2.Helper.Bowtie.ConvertToObjectFromJsonString<List<LocalApplication>>(json); // Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocalApplication>>(json);

                foreach(var a in list)
                {
                    _apps.Add(a.ID, a);
                }

            }

        }



        #endregion Constructors


        public void Delete(IApplication entity)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetAll()
        {
            return GetAll(0);
        }

        public IApplication[] GetAll(int? maxRecordCount)
        {
            return getall().ToArray();
        }

        public IApplication[] GetAPIAllowedApps()
        {
            throw new NotImplementedException();
        }

        public IApplication GetApplicationByAPIKey(string publicKey)
        {
            throw new NotImplementedException();
        }

        public IApplication[] GetApplicationsByClientID(string clientID)
        {
            throw new NotImplementedException();
        }

        public IApplication GetById(string id)
        {
            return getall().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(IApplication entity)
        {
            if (_apps.ContainsKey(entity.ID))
                _apps[entity.ID] = entity;
            else

            _apps.Add(entity.ID, entity);
          
        }

        public IApplication[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IApplication[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }


        public string ExportApplicationToJson(IApplication application)
        {
            ExportApplication a = new ExportApplication(application);

            a.Dewdrops = _uofw.DewdropRepository.GetByApplicationID(application.ID).Cast<BasicDewdrop>().Where(x => x.ApplicationID != 0.ToString()).ToList();
            a.Achievements = _uofw.AchievementRepository.GetByApplicationID(application.ID).Cast<BasicAchievement>().ToList();
            a.Leaderboards = _uofw.LeaderboardRepository.GetByApplicationID(application.ID).Cast<BasicLeaderboard>().ToList();
            a.InventoryItems = _uofw.InventoryRepository.GetByApplicationID(application.ID).Cast<BasicInventoryItem>().ToList();
            a.Activities = new List<Common.CodeNamePair>();
            var activites = _uofw.DictionaryRepository.GetApplicationActivities(application.ID);


            foreach(var act in activites)
            {
                a.Activities.Add(new Common.CodeNamePair(act.Key, act.Value));
            }

            
            foreach (var s in a.Achievements)
            {
                foreach (var s1 in _uofw.AchievementRepository.GetStepsByAchievementID(s.ID))
                {
                    a.AchievementStepRules.Add(s1);
                }

            }
            return JB2.Helper.Bowtie.ConvertToJsonString(a);
        }

        public IApplication ImportApplicationFromJson(string json)
        {



            var a = JB2.Helper.Bowtie.ConvertToObjectFromJsonString<ExportApplication>(json);

            _uofw.ApplicationRepository.Insert(a);

            foreach (var d in a.Dewdrops)
            {
                _uofw.DewdropRepository.Insert(d);
            }

            foreach(var c in a.Achievements)
            {
                _uofw.AchievementRepository.Insert(c);
            }

            foreach(var s in a.AchievementStepRules)
            {
                _uofw.AchievementRepository.Insert(s);
            }

            foreach(var l in a.Leaderboards)
            {
                _uofw.LeaderboardRepository.Insert(l);
            }

            foreach(var i in a.InventoryItems)
            {
                _uofw.InventoryRepository.Insert(i);
            }

            Dictionary<string, string> actdic = new Dictionary<string, string>();

            foreach(var act in a.Activities)
            {
                actdic.Add(act.Code, act.Name);
            }

            _uofw.DictionaryRepository.InsertActivities(a.ID, actdic);
            return a;


        }


        #region Helpers

        public IEnumerable<IApplication> getall()
        {

            return _apps.Values.ToList();

            //var json = "[{ \"Website\":\"http://linkfence.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{ \"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a600dcba\",\"Name\":\"Link Fence\"},{ \"Website\":\"http://fivetwo.io\",\"IsAuthorized\":true,\"AuthorizedState\":3,\"Company\":{ \"POC\":null,\"MailingAddress\":null,\"ID\":\"jb2-centreville\",\"Name\":\"JBsquared LLC\"},\"APIkey\":null,\"Secret\":null,\"ID\":\"a4cc70f2\",\"Name\":\"FiveTwo\"},null]";

            //return Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocalApplication>>(json);


        }








        #endregion Helpers
    }
}
