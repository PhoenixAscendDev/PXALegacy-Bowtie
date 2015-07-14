using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class MasterLeaderboard : Leaderboard, IMasterLeaderboard, ILeaderboard, IBowtieObject
    {

        private string _appID;
        private Dictionary<Enum.LeaderboardType,ILeaderboard> _leaderboards;
        private List<Enum.LeaderboardType> _types;



        public string ApplicationID
        {
            get
            {
                return _appID;
            }
            set
            {
                _appID = value;
            }
        }

        public ILeaderboard[] Leaderboards
        {
            get
            {
                return _leaderboards.Values.ToArray();
            }
        }

        public Enum.LeaderboardType[] Types
        {
            get
            {
                return _types.ToArray();
            }
            set
            {
                _types = value.ToList();
            }
        }

        public ILeaderboard GetLeaderboard(Enum.LeaderboardType type)
        {
            if (_leaderboards.ContainsKey(type))
                return _leaderboards[type];
            else
                return null;
           
        }
    }
}
