using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;
using JB2.Common;
namespace JB2.Bowtie
{

    
    public abstract class Achievement : BowtieObject, IAchievement, IClass
    {
        protected string _appID;
        protected int _sortorder;
        protected string _description;
        protected Enum.AchievementType _achievementType;
        protected string _category;
        protected int _steps;
        protected Dictionary<string, string> _icons;
        protected Enum.AchievementRarityType _rarity;
        protected DateTime _timeStart;
        protected DateTime _timeEnd;
        
        protected string _stepRegEx;
        protected IEnumerable<string> _dewdropsTriggers;
        protected Enum.StepFxType _stepType;
        protected Dictionary<string, int> _points;

        public Achievement() : this(null)
        {
        }

        public Achievement(string id) : base(Enum.BowtieObjectType.bowtie_achievement, id)
        {
            _icons = new Dictionary<string, string>();
            _icons.Add("EARNED", string.Empty);
            _icons.Add("HIDDEN", string.Empty);
            _icons.Add("SHOW", string.Empty);
            _timeStart = DateTime.MinValue;
            _timeEnd = DateTime.MinValue;
            _dewdropsTriggers = new string[0];

        }


        public virtual string ApplicationID
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

        public virtual int SortOrder
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

        public virtual string Description
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

        public virtual Enum.AchievementType AchievementType
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

        public virtual string Category
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

        public virtual int StepsRequired
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

        public virtual string EarnedIconUrl
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

        public virtual string HiddenIconUrl
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

        public virtual string ShownIconUrl
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

        public virtual Enum.AchievementRarityType Rarity
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

        public virtual DateTime TimeBoundStart
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

        public virtual DateTime TimeBoundEnd
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

        public virtual string StepFx
        {
            get
            {
                return _stepRegEx;
            }

            set
            {
                _stepRegEx = value;
            }
        }


        public virtual Dictionary<string,int> Points
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
        public virtual IEnumerable<string> DewdropTriggers
        {
            get
            {
                return _dewdropsTriggers;
            }

            set
            {
                _dewdropsTriggers = value;
            }
        }

        public virtual StepFxType StepType
        {
            get
            {
                return _stepType;
            }

            set
            {
                _stepType = value;
            }
        }

        public IEnumerable<string> PointSystems
        {
            get
            {
                return _points.Keys.ToArray();
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        public int GetPoints(string pointSystemID)
        {
            return _points[pointSystemID];
        }
    }
}
