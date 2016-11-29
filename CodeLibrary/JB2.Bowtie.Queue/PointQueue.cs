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

        public ServiceResult PushPoint(IPlayerPoint playerPoint, IPointGiver pointGiver)
        {

            var pt = PointTransaction.FromPlayerPointGiver(playerPoint, pointGiver);

            PointQueueItem i = new PointQueueItem();
            i.Description = pointGiver.Description;
            i.GiverID = pointGiver.ID;
            i.GiverName = pointGiver.Name;
            i.GiverType = pointGiver.PointGiverType;
            i.PlayerID = playerPoint.GetPlayerID();
            i.Points = playerPoint.Points;
            i.PointSystemID = playerPoint.PointSystem;
            i.ValidationKey = pt.ValidationKey;

            var json = new JavaScriptSerializer().Serialize(i);

            return this.Push(json);

        }
    }
}
