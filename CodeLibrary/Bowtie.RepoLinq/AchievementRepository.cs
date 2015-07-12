using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data
{
    public class AchievementRepository : LinqRepository<IAchievement>, IAchievementRepository
    {

        public IPlayerAchievement[] GetPlayerAchievements(string playerID, string appID)
        {
            throw new NotImplementedException();
        }

        public bool SavePlayerAchievements(IPlayerAchievement playerAchievement)
        {
            throw new NotImplementedException();
        }

        public IAchievement[] GetAchievementsByApplication(string appID)
        {
            var query3 = from i in _dbcontext.jb2bt_Achievement_Get(null,appID)
                         select (IAchievement)getAchievement(i);
            return query3.ToArray();
        }
    }
}
