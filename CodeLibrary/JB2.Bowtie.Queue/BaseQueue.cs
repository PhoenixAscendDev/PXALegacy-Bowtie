using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using JB2.Common.Data;

namespace JB2.Bowtie.Queue
{
    public abstract class BaseQueue : IQueueRepo
    {

        #region Fields
        protected JB2.Common.Data.AzureQueueRepository _repo;
        #endregion Fields


        #region Constructor
        public BaseQueue(JB2.Common.Data.AzureQueueRepository repo)
        {
            _repo = repo;
        }

        #endregion Constructor


        public virtual IEnumerable<string> PeekAll()
        {
            throw new NotImplementedException();
        }

        public object Pop()
        {
            return _repo.Pop();
        }

        public ServiceResult Push(string item)
        {
            _repo.Push(item);

            return true;
        }
    }
}
