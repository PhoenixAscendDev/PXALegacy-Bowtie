using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IMaintenanceQueueRepo : IQueueRepo
    {
        JB2.Common.ServiceResult PushTask(string taskName);

    }
}
