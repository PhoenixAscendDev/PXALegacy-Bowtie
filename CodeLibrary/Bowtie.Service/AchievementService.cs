using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace JB2.Bowtie.Service
{
    public class AchievementService : GenericService<IAchievement,IAchievementRepository>
    {

 
        public AchievementService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {    
            
        }

        public AchievementService(IUnitOfWork unitOfWork) : this(unitOfWork.AchievementRepository)
        {
            _uofw = unitOfWork;
        }

        public AchievementService(IAchievementRepository repo)
            : base(repo)
        {

        }


        #region IAchievement
        public IAchievement[] RetrieveByApplication(string applicationid)
        {
            IAchievement[] result = null;
            try
            {
                result = _repo.GetAchievementsByApplication(applicationid);
            }
            catch
            {
                result = new IAchievement[0];
            }

            return result;
        }

        public IAchievement[] RetrieveByType(string applicationid, Enum.AchievementType type)
        {
            List<IAchievement> result = null;
            try
            {
                var list  = _repo.GetAchievementsByApplication(applicationid);
                result = list.ToList().FindAll(x => x.AchievementType == type);
            }
            catch
            {
                result = new List<IAchievement>();
            }

            return result.ToArray();
        }

        #endregion IAchievement


        #region PlayerAchievements

        public IPlayerAchievement[] InitilizePlayerAchievements(IBowtiePlayer player, IApplication app)
        {
            var paList = _repo.GetPlayerAchievements(player.GetPlayerID(), app.GetID());

            var aList = _repo.GetAchievementsByApplication(app.GetID());

            foreach(var a in aList)
            {
                //if doesn't exist then create it and add it
                if(paList.ToList().Find(x => x.AchievementID == a.GetID()) == null)
                {
                    var newpa = PlayerAchievement.New(player.GetPlayerID(), a);
                    _repo.Insert(newpa);
                }
            }

            paList = _repo.GetPlayerAchievements(player.GetPlayerID(), app.GetID());

            return paList;


        }


        public IPlayerAchievement  CalculateAchievement(IBowtiePlayer player, string achievementid, IPlayerDewdrop dewdrop)
        {
            var pa = _repo.GetPlayerAchievement(player.GetPlayerID(), achievementid);

            if (pa != null)
            {
                var a = _repo.GetById(pa.AchievementID);

                switch (a.StepType)
                {
                    case Enum.StepFxType.Empty:
                        pa.CurrentStep++;
                        break;
                    case Enum.StepFxType.RegexMatchSingle:
                        Match match = Regex.Match(dewdrop.GetValue(), a.StepFx, RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            pa.CurrentStep++;
                        }
                        break;
                    case Enum.StepFxType.DewdropCount:                  
                        int? count = player.DewdropCounts[dewdrop.GetID()];
                        if (count != null && count.GetValueOrDefault() >= Convert.ToInt32(a.StepFx))
                            pa.CurrentStep++;
                        break;
                }


                //if we achieved then set it
                if(!pa.AchievementFlags.Contains(Enum.AchievementFlag.Earned) && pa.CurrentStep >= a.StepsRequired)
                {
                    pa.Achieve(a.Points);
                }
            }

            _repo.Insert(pa);

            return pa;

           
        } 


        public IPlayerAchievement[] RetrievePlayerAchievement(string playerid, string applicationid)
        {
            throw new NotImplementedException();
        }

        public IPlayerAchievement[] RetrievePlayerAchievementByPlayer(string playerid)
        {
            throw new NotImplementedException();
        }

        public IPlayerAchievement[] RetrievePlayerAchievementByApplication(string applicationid)
        {
            throw new NotImplementedException();
        }
        public IPlayerAchievement RetrievePlayerAchievementByID(string id)
        {
            throw new NotImplementedException();

        }

        public void Save(IPlayerAchievement pa)
        {
            _repo.Insert(pa);
        }


        #endregion PlayerAchievements



    }
}
