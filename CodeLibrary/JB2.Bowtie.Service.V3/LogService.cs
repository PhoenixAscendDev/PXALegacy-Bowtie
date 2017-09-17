using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie.Service
{
    public class LogService : JB2.Common.Singleton<LogService>
    {
        #region Fields

        protected IUnitOfWork _uofw;

        #endregion Fields

        #region Constructors
        public LogService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public LogService(IUnitOfWork unitofwork)
        {
            _uofw = unitofwork;
        }

        #endregion Constructors


        public void LogError(Exception ex, string message= "", string logcode = "")
        {
            var logger = JB2.Settings.Bowtie.Logger;

            if (logger != null)
            {
                logger.EntryLogged -= onEntryLogged;
                logger.EntryLogged += onEntryLogged;

                message = String.IsNullOrEmpty(message) ? ex.Message : message;

                logger.LogError(ex, message, logcode);
            }

        }

        private void onEntryLogged(ILogger<JB2.Common.Enum.LogServerityType,string,ILogEntry> logger,JB2.Common.Enum.LogServerityType serverity, ILogEntry entry)
        {
            JB2.Events.Bowtie.OnLogEntryLogged(logger, entry);
        }



    }
}
