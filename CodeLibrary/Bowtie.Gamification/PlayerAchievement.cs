using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class PlayerAchievement : BowtieObject, IPlayerAchievement
    {
        protected string _playerid;
        protected string _achievementid;
        protected int _currentStep;
        protected Enum.AchievementFlag[]  _flags;
        protected int _points;


        public PlayerAchievement() :this(null)
        {

        }

        public PlayerAchievement(string id) : base(Enum.BowtieObjectType.bowtie_playerachievement,id)
        {

        }


        public string PlayerID
        {
            get
            {
                return _playerid;
            }
            set
            {
                _playerid = value;
            }
        }

        public string AchievementID
        {
            get
            {
                return _achievementid;
            }
            set
            {
                _achievementid = value;
            }
        }

        public int CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                _currentStep = value;
            }
        }

        public Enum.AchievementFlag[] AchievementFlags
        {
            get
            {
                return _flags;
            }
            set
            {
                _flags = value;
            }
        }

        public int PointsEarned
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }
    }
}
