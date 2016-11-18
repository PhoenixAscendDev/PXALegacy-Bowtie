using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

using JB2.Common.Data;

namespace JB2.Bowtie.Queue
{
    public class MaintenanceQueue : BaseQueue, IMaintenanceQueueRepo
    {

        #region Constructor

        public MaintenanceQueue(AzureQueueRepository repo) : base(repo)
        {

        }

        #endregion Constructor
        public ServiceResult PushTask(string taskName)
        {
            return Push(taskName);
        }
    }
}
