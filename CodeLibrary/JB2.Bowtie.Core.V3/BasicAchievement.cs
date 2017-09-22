using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie
{
    public class BasicAchievement : BowtieObject, IAchievement
    {

        public BasicAchievement() : base()
        {
            _kind = BowtieObjectType.bowtie_achievement;
            TimeBoundStart = DateTime.MinValue;
            TimeBoundEnd = DateTime.MaxValue;
            //DewdropTriggers = new List<string>();

        }

        public string ApplicationID { get; set; }
        public int SortOrder { get; set; }
        public AchievementType AchievementType { get; set; }
        public string Category { get; set; }
        public int StepsRequired { get; set; }
        public string EarnedIconUrl { get; set; }
        public string HiddenIconUrl { get; set; }
        public string ShownIconUrl { get; set; }
        public DateTime TimeBoundStart { get; set; }
        public DateTime TimeBoundEnd { get; set; }
        //public string StepFx { get; set; }
        //public StepFxType StepType { get; set; }
        //public IEnumerable<string> DewdropTriggers { get; set; }
        public int Points { get; set; }

        public byte StorageSlot { get; set; }
    }
}
