using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface ILogRepository : JB2.Common.IRepository<JB2.Common.ILogEntry, string>
    {
        void Insert(IActivityEntry pa);

        void Insert(IPointEntry p);

        void Insert(ILeaderboardEntry e);

       // void Insert(IAchievementEntry pa);

        IEnumerable<IActivityEntry> GetPlayerActivityByApplicationID(string applicationID, string playerID);

        IEnumerable<IPointEntry> GetPointEntryByApplicationID(string applicationID, string playerID);

        IEnumerable<ILeaderboardEntry> GetScoresByLeaderboard(string leaderboardID);

        IEnumerable<ILeaderboardEntry> GetScoresByPlayerID(string leaderboardID, string playerID);

        IEnumerable<ILeaderboardEntry> GetScoresByPlayerID(string playerID);

        IEnumerable<ILeaderboardEntry> GetScoresByApplicationID(string applicationID);

        IEnumerable<ILeaderboardEntry> GetScores(IApplicationPlayerPair<string, string> pair);

      //  IEnumerable<IAchievementEntry> GetPlayerAchievementByApplicationID(string applicationID, string playerID);
    }
}
