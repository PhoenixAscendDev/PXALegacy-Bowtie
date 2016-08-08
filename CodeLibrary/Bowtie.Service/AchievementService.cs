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


        public IPlayerAchievement  CalculateAchievement(string playerid, string achievementid, object value)
        {
            var pa = _repo.GetPlayerAchievement(playerid, achievementid);

            if (pa != null)
            {
                var a = _repo.GetById(pa.AchievementID);

                switch (a.StepType)
                {
                    case Enum.StepFxType.Empty:
                        pa.CurrentStep++;
                        break;
                    case Enum.StepFxType.RegexMatchSingle:
                        Match match = Regex.Match(value.ToString(), a.StepFx, RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            pa.CurrentStep++;
                        }
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


        IPlayerAchievement[] RetrievePlayerAchievement(string playerid, string applicationid)
        {
            throw new NotImplementedException();
        }

        IPlayerAchievement[] RetrievePlayerAchievementByPlayer(string playerid)
        {
            throw new NotImplementedException();
        }

        IPlayerAchievement[] RetrievePlayerAchievementByApplication(string applicationid)
        {
            throw new NotImplementedException();
        }
        IPlayerAchievement RetrievePlayerAchievementByID(string id)
        {
            throw new NotImplementedException();

        }

        IAchievement[] RetrieveByApplication(string applicationid)
        {
            throw new NotImplementedException();
        }
    }
}
