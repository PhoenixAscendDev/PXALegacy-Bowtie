using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Queue
{
    [Serializable]
    public class AchievementQueueItem
    {
       public string PlayerID { get; set; }
       public string AchievementID { get; set; }
       public string ApplicationID { get; set; }

       public long AchievedDateTimeTicks { get; set; }

       public string[] Flags { get; set; }

       public string Points { get; set; }
    
       public string PointSystem { get; set; }
    }
}
