using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IDewdropRepository : JB2.Common.IRepository<JB2.Bowtie.Dewdrop, string>
    {
        IEnumerable<Dewdrop> GetByApplicationID(string appID);
    }
}
