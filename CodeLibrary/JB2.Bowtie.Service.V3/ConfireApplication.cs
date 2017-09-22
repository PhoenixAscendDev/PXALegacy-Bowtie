using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class ExportApplication : BasicApplication, IApplication
    {

        public ExportApplication()
        {
            Dewdrops = new List<BasicDewdrop>();
            Achievements = new List<BasicAchievement>();
            AchievementStepRules = new List<AchievementStepRule>();
        }

        public ExportApplication(IApplication a) : this()
        {
            this.APIkey = a.APIkey;
            //this.AuthorizedState = a.AuthorizedState;
            this.Company = a.Company;
            this.ID = a.ID;
            this.Name = a.Name;
            this.RNG = a.RNG;
            this.Secret = a.Secret;
            this.Tags = a.GetTags().ToArray();
            this.Website = a.Website;
        }

        public List<BasicDewdrop> Dewdrops { get; set; }

        public List<BasicAchievement> Achievements { get; set; }

        public List<AchievementStepRule> AchievementStepRules { get; set; }

    }
}
