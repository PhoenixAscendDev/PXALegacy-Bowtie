using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class LapelPin : BaseAchievement, IAchievement
    {
        public LapelPin()
        {

        }

        public LapelPin(string id): base(id)
        {
            base.AchievementType = Enum.AchievementType.LabelPin;
        }

    }
}
