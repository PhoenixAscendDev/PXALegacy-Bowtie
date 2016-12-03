using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IPointQueueRepository : IQueueRepo
    {
        JB2.Common.ServiceResult PushPointTran(PointTransaction pt);
    }
}
