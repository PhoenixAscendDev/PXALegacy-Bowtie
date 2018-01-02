using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IQueueRepo
    {
        IEnumerable<string> PeekAll();

        object Pop();

        JB2.Common.ServiceResult Push(string item);
    }
}
