using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class LeaderboardService :  GenericService<ILeaderboard,ILeaderboardRepository>
    {
        public LeaderboardService()
        {
           
        }

        public LeaderboardService(IUnitOfWork unitOfWork) : this(unitOfWork.LeaderboardRepository)
        {
            _uofw = unitOfWork;
        }

        public LeaderboardService(ILeaderboardRepository repo)
            : base(repo)
        {

        }

        IMasterLeaderboard RetrieveMasterByID(string id)
        {
            return _uofw.LeaderboardRepository.GetMasterByID(id);
            
        }

        IMasterLeaderboard[] RetrieveMasterByApplicationID(string applicationID)
        {
            return _uofw.LeaderboardRepository.GetMasterAll();

        }

    }
}
