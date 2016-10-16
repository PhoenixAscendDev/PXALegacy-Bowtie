using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Common.Log;

namespace JB2.Economy
{
    public static class jBeanLogExtensions
    {
        public static void jBeanLog(this Exception exception)
        {
            var factory = JB2.Settings.Jbean.Factory;
            ILogger logger = (ILogger)factory.Logger;
            //logger.Log(new JB2.Common.Log.LogEntry("testID", Common.Enum.LogServerityType.Error, exception.Message, exception, System.DateTime.Now));
            logger.Log(Common.Enum.LogServerityType.Error, exception);
            //logger.LogError(exception);            
        }
    }
}
