using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class AchievementStepRule
    {

        public AchievementStepRule()
        {
            IsActive = true;
            ID = JB2.Helper.Bowtie.GenerateID<AchievementStepRule>();
        }

        public string ID { get; set; }
        public string AchievementID { get; set; }
        public Enum.StepFxType StepType { get; set; }
        public string StepFx { get; set; }

        public bool IsActive { get; set; }

        public static AchievementStepRule NewDewdropValueRule(string achievementID,string GDID, Enum.Comparisons compare, int value)
        {
            AchievementStepRule r = new AchievementStepRule();

            r.AchievementID = achievementID;
            r.StepType = Enum.StepFxType.DewdropValue;
            r.StepFx = GDID + "|" + ((int)compare).ToString() + "|" + value.ToString();
            r.IsActive = true;

            return r;
        }

        public static AchievementStepRule NewDewdropIncrementRule(string achievementID, string GDID, int multiplier = 1)
        {
            AchievementStepRule r = new AchievementStepRule();

            r.AchievementID = achievementID;
            r.StepType = Enum.StepFxType.DewdropIncrement;
            r.StepFx = GDID + "|" + multiplier.ToString();
            r.IsActive = true;

            return r;
        }

        public static AchievementStepRule NewRegExRule(string achievementID, string expression,string property )
        {
            AchievementStepRule r = new AchievementStepRule();
            r.AchievementID = achievementID;
            r.StepType = Enum.StepFxType.RegexMatchSingle;
            r.StepFx = property + "|" + expression;

            return r;
        }


    }
}
