using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Queue
{
    [Serializable]
    public class PointQueueItem
    {
        
        public string PlayerID { get; set; }
        public int Points { get; set; }
        public string PointSystemID { get; set; }

        public string GiverID { get; set; }
        public string GiverName { get; set; }
        public string GiverType { get; set; }

        public string Description { get; set; }

        public string ValidationKey { get; set; }
    }
}
