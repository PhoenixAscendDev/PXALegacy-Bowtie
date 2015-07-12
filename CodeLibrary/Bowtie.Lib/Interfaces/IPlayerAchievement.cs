using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IPlayerAchievement : IBowtieObject , JB2.Common.IIDNamePair<string,string>
    {
        string PlayerID { get; set; }
        string AchievementID { get; set; }
        int CurrentStep { get; set; }
        Enum.AchievementFlag[] AchievementFlags { get; set; }
        int PointsEarned { get; set; }
    }
}
