using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.NoDB
{
    public class AchievementRepository : JB2.Bowtie.IAchievementRepository
    {

        public IPlayerAchievement[] GetPlayerAchievements(string playerID, string appID)
        {
            throw new NotImplementedException();
        }

        public void Insert(IPlayerAchievement playerAchievement)
        {
            throw new NotImplementedException();
        }

        public void Delete(IAchievement entity)
        {
            throw new NotImplementedException();
        }

        public IAchievement[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IAchievement GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(IAchievement entity)
        {
            throw new NotImplementedException();
        }

        public IAchievement[] SearchFor()
        {
            throw new NotImplementedException();
        }


        public IAchievement[] GetAchievementsByApplication(string appID)
        {
            throw new NotImplementedException();
        }

        public IAchievement[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }

        public IPlayerAchievement GetPlayerAchievement(string playerID, string achievementID)
        {
            throw new NotImplementedException();
        }
    }
}
