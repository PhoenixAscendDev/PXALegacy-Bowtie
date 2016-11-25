using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Queue
{
    public static class QueueItemExtension
    {

        public static JB2.Bowtie.Queue.AchievementQueueItem ToQueueItem(this JB2.Bowtie.IPlayerAchievement pa)
        {

            var aRepo = JB2.Settings.Bowtie.UnitOfWork.AchievementRepository;
            var app = aRepo.GetById(pa.AchievementID);

            JB2.Bowtie.Queue.AchievementQueueItem i = new AchievementQueueItem();

            i.AchievedDateTimeTicks = pa.DateAchieved.Ticks;
            i.AchievementID = pa.AchievementID;
            i.ApplicationID = app.ApplicationID;


            List<string> flags = new List<string>(pa.AchievementFlags.Count());
            foreach(var f in pa.AchievementFlags)
            {
                flags.Add(f.ToString());
            }

            i.Flags = flags.ToArray();
            i.PlayerID = pa.GetPlayerID();
            i.Points = app.Points.ToString();
            i.PointSystem = "bitscore";
            return i;
        }


    }
}
