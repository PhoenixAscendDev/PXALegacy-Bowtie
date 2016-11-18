using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IDewdropQueueRepo : IQueueRepo
    {
        JB2.Common.ServiceResult PushDewdrop(IPlayerDewdrop pdewdrop);
    }
}
