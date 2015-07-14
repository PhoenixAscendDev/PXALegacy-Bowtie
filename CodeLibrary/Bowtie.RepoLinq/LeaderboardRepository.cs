using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Linq
{
    public class LeaderboardRepository : LinqRepository<ILeaderboard>, ILeaderboardRepository
    {
        private LinqRepository<IMasterLeaderboard> _masterAchievementRepo;

        public LeaderboardRepository(BowtieDataContext context): base(context, Enum.BowtieObjectType.bowtie_leaderboard)
        {
            _masterAchievementRepo = new LinqRepository<IMasterLeaderboard>(context, Enum.BowtieObjectType.bowtie_masterLeaderboard);

        }

        public IMasterLeaderboard[] GetMasterAll()
        {
            return _masterAchievementRepo.GetAll();
        }

        public IMasterLeaderboard GetMasterByID(string id)
        {
            return _masterAchievementRepo.GetById(id);
        }

        public IMasterLeaderboard[] GetMasterByApplicationID(string id)
        {
            var query7 = from i in _dbcontext.jb2bt_Leaderboard_Get(id, null)
                         select (IMasterLeaderboard)getMasterLeaderboard(i);
            return query7.ToArray();
        }
    }
}
