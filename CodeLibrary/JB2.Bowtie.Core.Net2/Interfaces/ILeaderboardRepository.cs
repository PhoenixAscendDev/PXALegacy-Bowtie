using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface ILeaderboardRepository : JB2.Common.IRepository<JB2.Bowtie.ILeaderboard, string>
    {
        IMasterLeaderboard[] GetMasterAll();

        IMasterLeaderboard GetMasterByID(string id);

        IMasterLeaderboard[] GetMasterByApplicationID(string id);

    }
}
