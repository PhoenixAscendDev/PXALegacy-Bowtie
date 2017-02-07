using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IPlayerAchievement : IBowtieObject , JB2.Common.IIDNamePair<string,string>, IPlayerable<string>
    {
        string PlayerID { get; set; }
        string AchievementID { get; set; }
        int CurrentStep { get; set; }
        Enum.AchievementFlag[] AchievementFlags { get; set; }
        int GetPointsEarned(string pointSystemId);

        IEnumerable<string> GetPointSystems();
        
        void Achieve();

        DateTime DateAchieved { get; set; }

        bool isAchieved { get; }

    }
}
