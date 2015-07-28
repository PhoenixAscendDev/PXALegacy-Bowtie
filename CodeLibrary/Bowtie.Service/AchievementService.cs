using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class AchievementService : GenericService<IAchievement,IAchievementRepository>
    {

        public AchievementService()
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
