using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface ILogRepository : JB2.Common.IRepository<JB2.Common.ILogEntry, string>
    {
        void Insert(IActivityEntry pa);

        IEnumerable<IActivityEntry> GetPlayerActivityByApplicationID(string applicationID, string playerID);
    }
}
