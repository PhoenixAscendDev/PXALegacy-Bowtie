using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JB2.Bowtie.Web.Models
{
    public class AchievementViewModel
    {
        public string ApplicationId {get; set;}
        public string Description { get; set; }
        public int StepsRequired { get; set; }
        public int PointsEarned { get; set; }

        public string AchievementType { get; set; }
        public DateTime EventDateEndStart { get; set; }
        public DateTime EventDateEnd { get; set; }


    }
}