using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;

namespace JB2.Bowtie.Data
{
    public class LinqRepository<Tobject> : BaseRespository, JB2.Common.IRepository<Tobject, string> where Tobject : IBowtieObject
    {
        protected BowtieDataContext _dbcontext;
        private const string DB_PREFIX = "jb2bt_";
        protected JB2.Bowtie.Enum.BowtieObjectType _objType;

        public LinqRepository(BowtieDataContext dbc, JB2.Bowtie.Enum.BowtieObjectType type)
        {
            this._dbcontext = dbc;
            this._objType = type;

        }

        public LinqRepository()
        {
            this._dbcontext = new BowtieDataContext();
           
        }

        public void Delete(Tobject entity)
        {
            throw new NotImplementedException();
        }

        public Tobject[] GetAll()
        {
            switch(_objType)
            {
                case Enum.BowtieObjectType.bowtie_application:
                    var query = from i in _dbcontext.jb2bt_Application_Get(null, null)
                        select (Tobject)getApplication(i);
                    return query.ToArray();
                case Enum.BowtieObjectType.bowtie_client:
                    var query2 = from i in _dbcontext.jb2bt_Client_Get(null, null)
                                select (Tobject)getClient(i);
                    return query2.ToArray();
                case Enum.BowtieObjectType.bowtie_achievement:
                     var query3 = from i in _dbcontext.jb2bt_Achievement_Get(null,null)
                                select (Tobject)getAchievement(i);
                    return query3.ToArray();
            }
            return default(Tobject[]);
        }

        public Tobject GetById(string id)
        {
            switch(_objType)
            {
                case Enum.BowtieObjectType.bowtie_application:
                    var query = from i in _dbcontext.jb2bt_Application_Get(null,id)
                        select getApplication(i);
                    return (Tobject)query.ToArray().FirstOrDefault();
                case Enum.BowtieObjectType.bowtie_client:
                    var query2 = from i in _dbcontext.jb2bt_Client_Get(null, null)
                                 select (Tobject)getClient(i);
                    return query2.ToArray().FirstOrDefault();
                case Enum.BowtieObjectType.bowtie_achievement:
                     var query3 = from i in _dbcontext.jb2bt_Achievement_Get(id,null)
                                select (Tobject)getAchievement(i);
                     return query3.ToArray().FirstOrDefault();
            }
            return default(Tobject);
            //throw new NotImplementedException();
        }

        public void Insert(Tobject entity)
        {
            throw new NotImplementedException();
        }

        public Tobject[] SearchFor()
        {
            throw new NotImplementedException();
        }

        internal static IApplication getApplication<T>(T r) where T : class
        {
            Application result = new Application(getString(r, "PublicKey"), getString(r, "Secret"))
            {
                ID = getString(r, "ID"),
                Name = getString(r, "Name"),
                ClientID = getString(r,"Client_Key")
            };
            return result;
        }

        internal static IClient getClient<T>(T r) where T : class
        {
            Client result = new Client(getString(r, "Key"))
            {
                Name = getString(r, "Name"),
            };
            return result;
        }

        internal static IAchievement getAchievement<T>(T r) where T : class
        {
            IAchievement result = null;
            Enum.AchievementType type = (Enum.AchievementType)getInt(r, "AchievementType");

            switch(type)
            {
                case Enum.AchievementType.Standard :
                    result = new JB2.Bowtie.StandardAchievement(getString(r, "ObjectKey"));
                    break;
                case Enum.AchievementType.LabelPin :
                    result = new JB2.Bowtie.LapelPin(getString(r, "ObjectKey"));
                    break;
                case Enum.AchievementType.TimeBound:
                    result = new JB2.Bowtie.EventAchievement(getString(r, "ObjectKey"));
                    break;
            }

            result.ApplicationID = getString(r, "Application_Key");
            result.Category = getString(r, "Category");
            result.Description = getString(r, "Description");
            result.EarnedIconUrl = getString(r, "IconUrlEarned");
            result.HiddenIconUrl = getString(r, "IconUrlHidden");
            result.Name = getString(r, "Name");
            result.Points = getInt(r, "PointsWorth");
            result.Rarity = (Enum.AchievementRarityType)getInt(r, "RarityLevel");
            result.ShownIconUrl = getString(r, "IconUrlShown");
            result.SortOrder = getInt(r, "SortOrder");
            result.StepsRequired = getInt(r, "StepsRequire");
            result.TimeBoundEnd = getDate(r, "EventStartTime");
            result.TimeBoundStart = getDate(r, "EventEndTime");
            return result;
        }

    }
}
