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
            logger.LogError(exception);            
        }
    }
}
