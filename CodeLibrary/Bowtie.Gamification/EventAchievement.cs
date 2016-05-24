using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
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
