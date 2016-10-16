using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Economy
{
    public class jBeanLogger : JB2.Common.Log.Logger
    {
        #region Fields

        JB2.Common.Log.ILogRepo _repo;

        #endregion Fields

        #region Constructor
        public jBeanLogger(JB2.Common.Log.ILogRepo repo)
        {
            _repo = repo;
        }

        #endregion Constructor
        public override void Log(ILogEntry entry)
        {
            _repo.StoreLogEntry(entry);
        }
    }
}
