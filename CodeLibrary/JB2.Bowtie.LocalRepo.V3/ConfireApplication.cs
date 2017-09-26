using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace JB2.Bowtie.Data
{
    public class ExportApplication : BasicApplication, IApplication
    {

        public ExportApplication()
        {
            Dewdrops = new List<BasicDewdrop>();
            Achievements = new List<BasicAchievement>();
            AchievementStepRules = new List<AchievementStepRule>();
            Leaderboards = new List<BasicLeaderboard>();
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


        [JsonConverter(typeof(ConcreteConverter<JB2.Common.Business>))]
        public new JB2.Common.IBusiness Company
        {
            get
            {
                return base.Company;
            }
            set
            {
                base.Company = value;
            }
        }

        public List<BasicDewdrop> Dewdrops { get; set; }

        public List<BasicAchievement> Achievements { get; set; }

        public List<AchievementStepRule> AchievementStepRules { get; set; }

        public List<BasicLeaderboard> Leaderboards { get; set; }

    }
}
