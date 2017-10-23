using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface ILeaderboardEntry: IPlayerable<string>, IApplicationable<string>, ILeaderboardable<string>
    {
        string ID { get; set; }
        DateTime ScoreDate { get; set; }

        LeaderboardScore Score { get; set; }
        
    }
}
