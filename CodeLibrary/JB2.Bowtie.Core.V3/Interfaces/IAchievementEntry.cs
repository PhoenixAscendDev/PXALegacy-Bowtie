using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IAchievementEntry
    {
        string AchievementID { get; set; }
        string PlayerID { get; set; }
        string ApplicationID { get; set; }
        DateTime DateEarned { get; set; }
        int PointsEarned { get; set; }
        decimal PercentComplete { get; set; }

    }
}
