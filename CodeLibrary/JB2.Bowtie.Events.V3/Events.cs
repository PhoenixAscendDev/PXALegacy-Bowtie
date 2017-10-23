using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie;

namespace JB2.Events
{
    public class Bowtie : JB2.Common.Singleton<Bowtie>
    {

        public event Action<ILogger<JB2.Common.Enum.LogServerityType, string, ILogEntry>, ILogEntry> LogEntryLogged;

        public event Action<JB2.Bowtie.DewdropData, JB2.Bowtie.IDewdropEntry> DewdropDataUpdated;

        public event Action<JB2.Bowtie.IAchievement, JB2.Bowtie.IAchievementEntry> AchievementAchieved;

        public event Action<JB2.Bowtie.IActivityEntry, JB2.Bowtie.IPlayer, JB2.Bowtie.IApplication> NewPlayerActivity;

        public event Action<JB2.Bowtie.IPointEntry, JB2.Bowtie.IPlayer, JB2.Bowtie.IApplication> NewPointEntry;

        public event Action<JB2.Bowtie.ILeaderboardEntry, JB2.Bowtie.IPlayer, JB2.Bowtie.ILeaderboard> NewLeaderboardScore;


        public static void OnLogEntryLogged(ILogger<JB2.Common.Enum.LogServerityType, string, ILogEntry> logger, ILogEntry entry)
        {
            if (JB2.Events.Bowtie.Instance.LogEntryLogged != null)
                JB2.Events.Bowtie.Instance.LogEntryLogged(logger, entry);
        }

        public static void OnDewdropDataUpdated(DewdropData data, IDewdropEntry entry)
        {
            if (JB2.Events.Bowtie.Instance.DewdropDataUpdated != null)
                JB2.Events.Bowtie.Instance.DewdropDataUpdated(data, entry);
        }

        public static void OnAchievementAchieved(JB2.Bowtie.IAchievement achievement, JB2.Bowtie.IAchievementEntry entry)
        {
            if (JB2.Events.Bowtie.Instance.AchievementAchieved != null)
                JB2.Events.Bowtie.Instance.AchievementAchieved(achievement, entry);
        }

        public static void OnNewPlayerActivity(JB2.Bowtie.IActivityEntry act, JB2.Bowtie.IPlayer player, JB2.Bowtie.IApplication application)
        {
            if (JB2.Events.Bowtie.Instance.NewPlayerActivity != null)
                JB2.Events.Bowtie.Instance.NewPlayerActivity(act, player, application);
        }

        public static void OnNewPointEntry(JB2.Bowtie.IPointEntry act, JB2.Bowtie.IPlayer player, JB2.Bowtie.IApplication application)

        {
            if (JB2.Events.Bowtie.Instance.NewPointEntry != null)
                JB2.Events.Bowtie.Instance.NewPointEntry(act, player, application);
        }

        public static void OnNewLeaderboardScore(JB2.Bowtie.ILeaderboardEntry entry, JB2.Bowtie.IPlayer player, JB2.Bowtie.ILeaderboard leaderboard)
        {
            if (JB2.Events.Bowtie.Instance.NewLeaderboardScore != null)
                JB2.Events.Bowtie.Instance.NewLeaderboardScore(entry, player, leaderboard);
        }



    }
}
