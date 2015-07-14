using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.NoDB
{
    public class LeaderboardRepository : ILeaderboardRepository
    {
        public IMasterLeaderboard[] GetMasterAll()
        {
            throw new NotImplementedException();
        }

        public IMasterLeaderboard GetMasterByID(string id)
        {
            throw new NotImplementedException();
        }


        public void Delete(ILeaderboard entity)
        {
            throw new NotImplementedException();
        }

        public ILeaderboard[] GetAll()
        {
            throw new NotImplementedException();
        }

        public ILeaderboard GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(ILeaderboard entity)
        {
            throw new NotImplementedException();
        }

        public ILeaderboard[] SearchFor()
        {
            throw new NotImplementedException();
        }


        public IMasterLeaderboard[] GetMasterByApplicationID(string id)
        {
            throw new NotImplementedException();
        }
    }
}
