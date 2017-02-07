using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using JB2.Common;
using JB2.Common.Log;

namespace JB2.Bowtie
{
    public static class jBeanLogExtensions
    {
        public static void BowtieLog(this Exception exception)
        {
            
            ILogger logger = (ILogger)JB2.Settings.Bowtie.Logger;

            if (logger != null)
            {
                ILogEntry e = JB2.Common.Log.LogEntry.NewLogEntry(Common.Enum.LogServerityType.Error, exception);
                //logger.Log(new JB2.Common.Log.LogEntry("testID", Common.Enum.LogServerityType.Error, exception.Message, exception, System.DateTime.Now));
                logger.Log(e);
                JB2.Events.Bowtie.OnExceptionOccured(exception, e);
            }
            //logger.LogError(exception);            
        }
    }
}
