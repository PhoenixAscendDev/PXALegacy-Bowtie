using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface ILeaderboard : IBowtieObject, JB2.Common.IIDNamePair<string, string>, IApplicationable<string> , ILeaderboardable<string>
    {

        string ApplicationID { get; set; }
        string IconUrl { get; set; }
        Enum.LeaderboardType Type { get; set; }
        int ListOrder { get; set; }
        Enum.NumberFormatType ScoreFormat { get; set; }
        long ScoreLowerLimit { get; set; }
        long ScoreUpperLimit { get; set; }
        Enum.ScoreOrderType ScoreOrderType { get; set; }
        DateTime DateRangeStart { get; set; }
        DateTime DateRangeEnd { get; set; }
        string ParentID { get; set; }

    }
}
