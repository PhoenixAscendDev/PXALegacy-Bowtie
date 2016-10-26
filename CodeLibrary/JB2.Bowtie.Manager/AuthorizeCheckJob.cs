using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie.Web
{
    public class AuthorizeCheckJob : JB2.Common.Scheduler.RepeatableJob
    {
        public override ServiceResult DoWork()
        {
            JB2.Settings.Bowtie.isAuthorized();

            return true;
        }

        public override int GetCoolDownSeconds()
        {
            return JB2.Settings.Bowtie.AuthCheckInterval;
        }

        public override int GetMaxCounter()
        {
            return 0;
        }
    }
}
