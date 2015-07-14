using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Leaderboard : BowtieObject, ILeaderboard, IBowtieObject
    {
        protected string _iconurl;
        protected Enum.LeaderboardType _type;
        protected int _listOrder;
        protected Enum.NumberFormatType _scoreType;
        protected long _scoreLowerLimit;
        protected long _scoreUpperLimit;
        protected Enum.ScoreOrderType _scoreOrderType;
        protected DateTime _dateStartTime;
        protected DateTime _dateEndTime;
        protected string _masterLeadboardID;


        public Leaderboard() : this(null)
        {

        }

        public Leaderboard(string id) : base(Enum.BowtieObjectType.bowtie_leaderboard,id)
        {

        }



        public string IconUrl
        {
            get
            {
                return _iconurl;
            }
            set
            {
                _iconurl = value;
            }
        }

        public Enum.LeaderboardType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public int ListOrder
        {
            get
            {
                return _listOrder;
            }
            set
            {
                _listOrder = value;
            }
        }

        public Enum.NumberFormatType ScoreFormat
        {
            get
            {
                return _scoreType;
            }
            set
            {
                _scoreType = value;
            }
        }

        public long ScoreLowerLimit
        {
            get
            {
                return _scoreLowerLimit;
            }
            set
            {
                _scoreLowerLimit = value;
            }
        }

        public long ScoreUpperLimit
        {
            get
            {
                return _scoreUpperLimit;
            }
            set
            {
                _scoreUpperLimit = value;
            }
        }

        public Enum.ScoreOrderType ScoreOrderType
        {
            get
            {
                return _scoreOrderType;
            }
            set
            {
                _scoreOrderType = value;
            }
        }

        public DateTime DateRangeStart
        {
            get
            {
                return _dateStartTime;
            }
            set
            {
                _dateStartTime = value;
            }
        }

        public DateTime DateRangeEnd
        {
            get
            {
                return _dateEndTime;
            }
            set
            {
                _dateEndTime = value;
            }
        }

        public string MasterLeaderboardID
        {
            get
            {
                return _masterLeadboardID;
            }
            set
            {
                _masterLeadboardID = value;
            }
        }
    }
}
