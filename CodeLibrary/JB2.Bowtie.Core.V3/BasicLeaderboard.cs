using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public class BasicLeaderboard : BowtieObject, ILeaderboard
    {
        public string ApplicationID {get;set;}
        public string IconUrl { get; set; }
        public LeaderboardType Type { get; set; }
        public int ListOrder { get; set; }
        public NumberFormatType ScoreFormat { get; set; }
        public long ScoreLowerLimit { get; set; }
        public long ScoreUpperLimit { get; set; }
        public ScoreOrderType ScoreOrderType { get; set; }
        public DateTime DateRangeStart { get; set; }
        public DateTime DateRangeEnd { get; set; }
        public string ParentID { get; set; }

        public string GetApplicationID()
        {
            return ApplicationID;
        }

        public string GetLeaderboardID()
        {
            return ID;
        }
    }
}
