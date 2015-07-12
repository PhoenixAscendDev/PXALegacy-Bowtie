using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class BaseAchievement : BowtieObject, IAchievement
    {
        private string _appID;
        private int _sortorder;
        private string _description;
        private Enum.AchievementType _achievementType;
        private string _category;
        private int _steps;
        private Dictionary<string, string> _icons;
        private Enum.AchievementRarityType _rarity;
        private DateTime _timeStart;
        private DateTime _timeEnd;
        private long _points;



        public BaseAchievement() : this(null)
        {

        }

        public BaseAchievement(string id) : base(Enum.BowtieObjectType.bowtie_achievement,id)
        {
            _icons = new Dictionary<string,string>();
            _icons.Add("EARNED", string.Empty);
            _icons.Add("HIDDEN", string.Empty);
            _icons.Add("SHOW", string.Empty);
            
        }



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

        public int SortOrder
        {
            get
            {
                return _sortorder;
            }
            set
            {
                _sortorder = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public Enum.AchievementType AchievementType
        {
            get
            {
                return _achievementType;
            }
            set
            {
                _achievementType = value;
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }

        public int StepsRequired
        {
            get
            {
                return _steps;
            }
            set
            {
                _steps = value;
            }
        }

        public string EarnedIconUrl
        {
            get
            {
                return _icons["EARNED"];
            }
            set
            {
                _icons["EARNED"] = value;
            }
        }

        public string HiddenIconUrl
        {
            get
            {
                return _icons["HIDDEN"];
            }
            set
            {
                _icons["HIDDEN"] = value;
            }
        }

        public string ShownIconUrl
        {
            get
            {
                return _icons["SHOW"];
            }
            set
            {
                _icons["SHOW"] = value;
            }
        }

        public Enum.AchievementRarityType Rarity
        {
            get
            {
                return _rarity;
            }
            set
            {
                _rarity = value;
            }
        }

        public DateTime TimeBoundStart
        {
            get
            {
                return _timeStart;
            }
            set
            {
                _timeStart = value;

            }
        }

        public DateTime TimeBoundEnd
        {
            get
            {
                return _timeEnd;
            }
            set
            {
                _timeEnd = value;
            }
        }

        public long Points
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
