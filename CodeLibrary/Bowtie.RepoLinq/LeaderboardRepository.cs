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
            throw new NotImplementedException();
        }

        public IMasterLeaderboard GetMasterByID(string id)
        {
            throw new NotImplementedException();
        }
    }
}
