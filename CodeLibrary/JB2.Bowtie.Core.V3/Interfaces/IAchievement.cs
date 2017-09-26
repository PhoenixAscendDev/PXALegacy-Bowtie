using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IAchievement : IBowtieObject, JB2.Common.IIDNamePair<string, string>, IApplicationable<string>
    {
        string ApplicationID { get; set; }
        int SortOrder { get; set; }
        Enum.AchievementType AchievementType { get; set; }
        string Category { get; set; }
        int StepsRequired { get; set; }
        string EarnedIconUrl { get; set; }
        string HiddenIconUrl { get; set; }
        string ShownIconUrl { get; set; }
        //Enum.AchievementRarityType Rarity { get; set; }
        DateTime TimeBoundStart { get; set; }
        DateTime TimeBoundEnd { get; set; }

        //string StepFx { get; set; }
        //Enum.StepFxType StepType { get; set; }
        //IEnumerable<string> DewdropTriggers { get; set; }

        int Points { get; set; }

        byte StorageSlot { get; set; }

        // IEnumerable<string> PointSystems { get; set; }
        // int GetPoints(string pointSystemID);
    }
}
