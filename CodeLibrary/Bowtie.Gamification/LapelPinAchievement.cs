using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class LapelPinAchievement : Achievement, IAchievement
    {
        public LapelPinAchievement(): this(null)
        {

        }

        public LapelPinAchievement(string id) : base(id)
        {
            //standard achievements are not timebased so default them to min and max dates
            base._timeStart = DateTime.MinValue;
            base._timeEnd = DateTime.MinValue;
            base.AchievementType = Enum.AchievementType.LabelPin;
           
        }
    }
}
