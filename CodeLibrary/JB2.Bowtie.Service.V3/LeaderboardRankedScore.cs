using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class LeaderboardRankedScore : ILeaderboardRankedScore
    {

        #region Fields

        private ILeaderboardEntry _entry;
        private int _rank;

        #endregion Fields


        public LeaderboardRankedScore(ILeaderboardEntry entry, int rank)
        {
            _entry = entry;
            _rank = rank;
        }

        public string ID { get => _entry.ID; set => _entry.ID = value; }

        public int Ranked { get => _rank; set => _rank = value; }
        public DateTime ScoreDate { get => _entry.ScoreDate; set => _entry.ScoreDate = value; }
        public LeaderboardScore Score { get => _entry.Score; set => _entry.Score = value; }

        public string GetApplicationID()
        {
            return _entry.GetApplicationID();
        }

        public string GetLeaderboardID()
        {
            return _entry.GetLeaderboardID();
        }

        public string GetPlayerID()
        {
            return _entry.GetPlayerID();
        }
    }
}
