using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;


using JB2.Common;
using JB2.Common.Data;

using JB2.Bowtie.Queue;

namespace JB2.Bowtie.Queue.Azure
{
    public class AchievementQueue : BaseQueue, IAchievementQueueRepo
    {
        public AchievementQueue(AzureQueueRepository repo) : base(repo)
        {
        }

        public ServiceResult PushAchievement(IPlayerAchievement a)
        {

            var item = a.ToQueueItem();
            var json = new JavaScriptSerializer().Serialize(item);




            return this.Push(json.Replace("'", "\""));
        }
    }
}
