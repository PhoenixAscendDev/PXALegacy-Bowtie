using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    [GraphStory("sD1275546")]
    public class StandardAchievement : Achievement, IAchievement
    {
        public StandardAchievement(): this(null)
        {

        }

        public StandardAchievement(string id) : base(id)
        {
            //standard achievements are not timebased so default them to min and max dates
            base._timeStart = DateTime.MinValue;
            base._timeEnd = DateTime.MinValue;
            base.AchievementType = Enum.AchievementType.Standard;


        }
    }
}
