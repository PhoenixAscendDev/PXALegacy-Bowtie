using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class BasicLeaderboardEntry : ILeaderboardEntry
    {


        public DateTime ScoreDate { get; set; }
        public LeaderboardScore Score { get; set; }

        public string ApplicationID {get;set;}
        public string LeaderboardID { get; set; }
        public string PlayerID { get; set; }

        public string GetApplicationID()
        {
            return ApplicationID;
        }

        public string GetLeaderboardID()
        {
            return LeaderboardID;
        }

        public string GetPlayerID()
        {
            return PlayerID;
        }
    }
}
