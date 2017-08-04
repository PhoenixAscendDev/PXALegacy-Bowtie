using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IQueueRepo
    {
        IEnumerable<string> PeekAll();

        object Pop();

        JB2.Common.ServiceResult Push(string item);
    }
}
