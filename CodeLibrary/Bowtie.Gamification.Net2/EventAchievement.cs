using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    [GraphStory("sD1275546")]
    public class EventAchievement : Achievement, IAchievement
    {
        public EventAchievement()
        {

        }

        public EventAchievement(string id) : base(id)
        {
            base._achievementType = Enum.AchievementType.TimeBound;
        }
    }
}
