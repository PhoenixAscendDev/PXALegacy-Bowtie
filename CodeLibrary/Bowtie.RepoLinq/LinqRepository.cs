using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;

namespace JB2.Bowtie.Data.Linq
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
                case Enum.BowtieObjectType.bowtie_playerachievement:
                    var query4 = from i in _dbcontext.jb2bt_Player_Achievement_Get(null,null,null)
                                 select (Tobject)getPlayerAchievement(i);
                    return query4.ToArray();
                case Enum.BowtieObjectType.bowtie_command:
                    var query5 = from i in _dbcontext.jb2bt_Game_Command_Get(null, null)
                                 select (Tobject)getGameCommand(i);
                    return query5.ToArray();
                case Enum.BowtieObjectType.bowtie_leaderboard:
                    var query6 = from i in _dbcontext.jb2bt_Leaderboard_Get(null, null)
                                 select (Tobject)getGameCommand(i);
                    return query6.ToArray();
                case Enum.BowtieObjectType.bowtie_masterLeaderboard:
                    var query7 = from i in _dbcontext.jb2bt_Leaderboard_Get(null, null)
                                 select (Tobject)getGameCommand(i);
                    return query7.ToArray();
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
                case Enum.BowtieObjectType.bowtie_playerachievement:
                     var query4 = from i in _dbcontext.jb2bt_Player_Achievement_Get(id,null,null)
                                  select (Tobject)getPlayerAchievement(i);
                     return query4.ToArray().FirstOrDefault();
                case Enum.BowtieObjectType.bowtie_command:
                     var query5 = from i in _dbcontext.jb2bt_Game_Command_Get(id, null)
                                  select (Tobject)getGameCommand(i);
                     return query5.ToArray().FirstOrDefault();
                case Enum.BowtieObjectType.bowtie_leaderboard:
                     var query6 = from i in _dbcontext.jb2bt_Leaderboard_Get(id,null)
                                  select (Tobject)getGameCommand(i);
                     return query6.ToArray().FirstOrDefault();
                case Enum.BowtieObjectType.bowtie_masterLeaderboard:
                     var query7 = from i in _dbcontext.jb2bt_Leaderboard_Get(id, null)
                                  select (Tobject)getGameCommand(i);
                     return query7.ToArray().FirstOrDefault();

            }
            return default(Tobject);
            //throw new NotImplementedException();
        }

        public void Insert(Tobject entity)
        {
            switch(_objType)
            {
                case Enum.BowtieObjectType.bowtie_achievement:
                    SaveAchievement( (IAchievement)entity, null);
                    break;
                case Enum.BowtieObjectType.bowtie_playerachievement:
                    SavePlayerAchievement( (IPlayerAchievement)entity,null);
                    break;
                case Enum.BowtieObjectType.bowtie_command:
                    SaveGameCommand((IGameCommand)entity, null);
                    break;
                case Enum.BowtieObjectType.bowtie_leaderboard:
                    SaveLeaderboard((ILeaderboard)entity, null);
                    break;
                case Enum.BowtieObjectType.bowtie_masterLeaderboard:
                    SaveMasterLeaderboard((IMasterLeaderboard)entity, null);
                    break;
                
            }
        }

        public void Update(Tobject entity)
        {
            ///Currently we are not seperating out Insert and Update into seperate SQL sp.
            this.Insert(entity);
        }


        public Tobject[] SearchFor()
        {
            throw new NotImplementedException();
        }

        #region Get Methods

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

        internal static IPlayerAchievement getPlayerAchievement<T>(T r) where T : class
        {

            ///TODO Bitwise thing with the Flags
            PlayerAchievement result = new PlayerAchievement(getString(r, "ObjectKey"))
            {
              //AchievementFlags = new Enum.AchievementFlag[](),
              Name = getString(r,"Name"),
              PlayerID = getString(r,"Player_Key"),
              AchievementID = getString(r,"Achievement_Key"),
               CurrentStep = getInt(r,"CurrentStep")
            };

            return result;
        }

        internal static IGameCommand getGameCommand<T>(T r) where T : class
        {

            IPlayer iplayer = new Player(getString(r,"Affected_Player_Key"));
            IPlayer aplayer = new Player(getString(r,"Affected_Player_Key"));
            ///TODO Bitwise thing with the Flags
            GameCommand result = new GameCommand(getString(r, "ObjectKey"))
            {
                //AchievementFlags = new Enum.AchievementFlag[](),
                Name = getString(r, "Name"),
                AffectedPlayer = aplayer,
                AppID = getString(r, "Application_Key"),
                CommandCode = getString(r, "CommandCode"),
                GameID = getString(r, "Game_Key"),
                IssuedPlayer = iplayer
            };
            return result;


        }

        internal static ILeaderboard getLeaderboard<T>(T r) where T : class
        {
            Leaderboard result = new Leaderboard(getString(r, "ObjectKey"))
            {
                Name = getString(r, "Name"),
                DateRangeEnd = getDate(r,"DateEventEnd"),
                DateRangeStart = getDate(r,"DateEventStart"),
                IconUrl = getString(r,"IconUrl"),
                ListOrder = getInt(r,"ListOrder"),
                MasterLeaderboardID = getString(r,"MasterLeaderboard_Key"),
                ScoreFormat = (Enum.NumberFormatType)getInt(r,"ScoreFormat"),
                ScoreLowerLimit = getInt(r,"ScoreLowerLimit"),
                ScoreUpperLimit = getInt(r,"ScoreUpperLimit"),
                ScoreOrderType = (Enum.ScoreOrderType)getInt(r,"ScoreOrderType"),
                Type = (Enum.LeaderboardType)getInt(r,"Type")
            };
            return result;

        }

        internal static IMasterLeaderboard getMasterLeaderboard<T>(T r) where T : class
        {
            MasterLeaderboard result = new MasterLeaderboard(getString(r, "ObjectKey"))
            {
                Name = getString(r, "Name"),
                DateRangeEnd = getDate(r, "DateEventEnd"),
                DateRangeStart = getDate(r, "DateEventStart"),
                IconUrl = getString(r, "IconUrl"),
                ListOrder = getInt(r, "ListOrder"),
                MasterLeaderboardID = getString(r, "MasterLeaderboard_Key"),
                ScoreFormat = (Enum.NumberFormatType)getInt(r, "ScoreFormat"),
                ScoreLowerLimit = getInt(r, "ScoreLowerLimit"),
                ScoreUpperLimit = getInt(r, "ScoreUpperLimit"),
                ScoreOrderType = (Enum.ScoreOrderType)getInt(r, "ScoreOrderType"),
                //Type = (Enum.LeaderboardType)getInt(r, "Type"),
                ApplicationID = getString(r,"ApplicationID")
            };
            return result;

        }



        #endregion

        #region Save Methods


        private bool SaveAchievement(IAchievement a,string mode)
        {
            jb2bt_Achievement_SaveResult result = _dbcontext.jb2bt_Achievement_Save(a.ID, a.Name, a.ApplicationID, a.SortOrder, a.Description, (int)a.AchievementType, a.Category, a.StepsRequired
                                              , a.EarnedIconUrl, a.HiddenIconUrl, a.ShownIconUrl,
                                              a.TimeBoundStart, a.TimeBoundEnd, (int)a.Points, mode).FirstOrDefault();
            return (result.Key == a.ID);
        }

        private bool SavePlayerAchievement(IPlayerAchievement a, string mode)
        {
            ///TODO: Find that bitwise to Int utility method (maybe in the JB2.Common
            jb2bt_Player_Achievement_SaveResult result = _dbcontext.jb2bt_Player_Achievement_Save(a.ID, a.Name, a.PlayerID, a.AchievementID, a.CurrentStep,
                                                                                                  (int)a.AchievementFlags.FirstOrDefault(), a.PointsEarned, mode).FirstOrDefault();

            return (result.Key == a.ID);          
        }

        private bool SaveGameCommand(IGameCommand a, string mode)
        {
            jb2bt_Game_Command_SaveResult result = _dbcontext.jb2bt_Game_Command_Save(a.ID, a.Name, a.AppID, a.GameID, a.IssuedPlayer.ID, a.AffectedPlayer.ID, a.CommandCode, mode).FirstOrDefault();

            return (result.Key == a.ID); 
        }

        private bool SaveLeaderboard(ILeaderboard a, string mode)
        {
            jb2bt_Leaderboard_SaveResult result = _dbcontext.jb2bt_Leaderboard_Save(a.ID, a.Name, a.IconUrl, (int)a.Type, a.ListOrder, (int)a.ScoreFormat,
                                                                                     a.ScoreLowerLimit, a.ScoreUpperLimit, (int)a.ScoreOrderType,
                                                                                     a.DateRangeStart, a.DateRangeEnd, a.MasterLeaderboardID, string.Empty, mode).FirstOrDefault();

            return (result.Key == a.ID);
        }

        private bool SaveMasterLeaderboard(IMasterLeaderboard a, string mode)
        {
            jb2bt_Leaderboard_SaveResult result = _dbcontext.jb2bt_Leaderboard_Save(a.ID, a.Name, a.IconUrl, (int)a.Type, a.ListOrder, (int)a.ScoreFormat,
                                                                                     a.ScoreLowerLimit, a.ScoreUpperLimit, (int)a.ScoreOrderType,
                                                                                     a.DateRangeStart, a.DateRangeEnd, a.MasterLeaderboardID, a.MasterLeaderboardID, mode).FirstOrDefault();

            return (result.Key == a.ID);
        }


        #endregion

    }
}
