using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Common.Log;

namespace JB2.Bowtie
{
    public static class jBeanLogExtensions
    {
        public static void BowtieLog(this Exception exception)
        {
            
            ILogger logger = (ILogger)JB2.Settings.Bowtie.Logger;
            //logger.Log(new JB2.Common.Log.LogEntry("testID", Common.Enum.LogServerityType.Error, exception.Message, exception, System.DateTime.Now));
            logger.Log(Common.Enum.LogServerityType.Error, exception);
            //logger.LogError(exception);            
        }
    }
}
