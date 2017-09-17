using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Configure
{
    public class Bowtie
    {


        public void AddLogger(ILogger logger)
        {
            JB2.Settings.Bowtie._logger = logger;

            setIsConfigured();
        }

        public void AddUnitOfWork(JB2.Bowtie.IUnitOfWork uofw)
        {
            JB2.Settings.Bowtie._unitofWork = uofw;

            setIsConfigured();
        }


        private void setIsConfigured()
        {
            try
            {
                if (JB2.Settings.Bowtie._unitofWork != null)
                    JB2.Settings.Bowtie._isConfigured = true;
                else
                    JB2.Settings.Bowtie._isConfigured = false;
            }
            catch (Exception ex)
            {
                JB2.Settings.Bowtie._isConfigured = false;
            }
        }
    }
}
