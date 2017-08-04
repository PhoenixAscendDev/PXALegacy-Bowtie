using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using JB2.Common.Data;

using System.Web.Script.Serialization;

namespace JB2.Bowtie.Queue
{
    public class PointQueue : BaseQueue, IPointQueueRepository
    {
        public PointQueue(AzureQueueRepository repo) : base(repo)
        {

        }

        public ServiceResult PushPointTran(PointTransaction pt)
        {

            

            PointQueueItem i = new PointQueueItem();
            i.Description = pt.Description;
            i.GiverID = pt.GiverID;
            i.GiverName = pt.GiverName;
            i.GiverType = pt.PointGiverType;
            i.PlayerID = pt.GetPlayerID();
            i.Points = pt.Points;
            i.PointSystemID = pt.PointSystem;
            i.ValidationKey = pt.ValidationKey;

            var json = new JavaScriptSerializer().Serialize(i);

            return this.Push(json);

        }
    }
}
