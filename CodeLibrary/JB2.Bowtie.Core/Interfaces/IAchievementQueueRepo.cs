using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IAchievementQueueRepo : IQueueRepo
    {
        JB2.Common.ServiceResult PushAchievement(IPlayerAchievement achievement);
    }
}
