using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IMasterLeaderboard : IBowtieObject, ILeaderboard, JB2.Common.IIDNamePair<string,string>
    {
        string ApplicationID { get; set; }
        ILeaderboard[] Leaderboards { get; }
        Enum.LeaderboardType[] Types { get; set; }


        ILeaderboard GetLeaderboard(Enum.LeaderboardType type);
    }
}
