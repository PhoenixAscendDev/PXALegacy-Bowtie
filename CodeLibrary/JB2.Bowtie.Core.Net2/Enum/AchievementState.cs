using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Bowtie.Enum
{
    public enum AchievementFlag
    {
        Hidden =1 << 0,
        ShownName =1 << 1,
        ShowDescription =1 << 2,
        ShowProcess =1 << 3,
        Earned = 1 << 4,
    }
}
