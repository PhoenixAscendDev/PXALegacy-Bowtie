using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public class AchievementEntry : IAchievementEntry
    {
        public string AchievementID { get; set; }
        public string PlayerID { get; set; }
        public string ApplicationID { get; set; }
        public DateTime DateEarned { get; set; }
        public int PointsEarned { get; set; }
        public decimal PercentComplete { get; set; }
        public AchievementStatusType Status { get; set; }
    }
}
