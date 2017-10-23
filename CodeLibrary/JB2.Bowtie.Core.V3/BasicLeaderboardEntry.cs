using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class BasicLeaderboardEntry : ILeaderboardEntry
    {

        #region Constructors

        public BasicLeaderboardEntry()
        {
            
        }

        #endregion Constructors
        public string ID { get; set; }
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

        public static BasicLeaderboardEntry New
        {
            get
            {
                var r = new BasicLeaderboardEntry();
                r.ID = JB2.Common.NewID.Guid();

                return r;
            }
        }
    }
}
