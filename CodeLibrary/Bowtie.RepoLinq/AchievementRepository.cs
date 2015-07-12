using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data
{
    public class AchievementRepository : LinqRepository<IAchievement>, IAchievementRepository
    {
        private LinqRepository<IPlayerAchievement> _playerAchievementRepo;

        public AchievementRepository(BowtieDataContext context): base(context, Enum.BowtieObjectType.bowtie_application)
        {
            _playerAchievementRepo = new LinqRepository<IPlayerAchievement>(context, Enum.BowtieObjectType.bowtie_playerachievement);

        }


        public IPlayerAchievement[] GetPlayerAchievements(string playerID, string appID)
        {
            var query4 = from i in _dbcontext.jb2bt_Player_Achievement_Get(null,playerID,appID)
                         select getPlayerAchievement(i);
            return query4.ToArray();
        }

        public bool SavePlayerAchievements(IPlayerAchievement playerAchievement)
        {
            _playerAchievementRepo.Insert(playerAchievement);
            return true;
            
        }

        public IAchievement[] GetAchievementsByApplication(string appID)
        {
            var query3 = from i in _dbcontext.jb2bt_Achievement_Get(null,appID)
                         select (IAchievement)getAchievement(i);
            return query3.ToArray();
        }
    }
}
