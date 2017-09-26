using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IInventoryRepository : JB2.Common.IRepository<JB2.Bowtie.IInventoryItem, string>
    {
        IEnumerable<IInventoryItem> GetByApplicationID(string applicationID);
    }
}
