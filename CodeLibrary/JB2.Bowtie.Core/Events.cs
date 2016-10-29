using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie;

namespace JB2.Events
{
    public class Bowtie
    {
        //logging
        public static event Action<ILogger<JB2.Common.Enum.LogServerityType, string, ILogEntry>, ILogEntry> LogEntryLogged;
        public static event Action<Exception, ILogEntry> ExceptionOccured;

        //application 
        public static event Action<IApplication, DateTime> ApplicationCreated;
        public static event Action<IApplication, string> ApplicationInitilized;

        //player
        public static event Action<IBowtiePlayer, IApplication, DateTime> PlayerSignedIn;

        public static void OnApplicationCreated(IApplication application, DateTime dateCreated)
        {
            if (ApplicationCreated != null)
                ApplicationCreated(application, dateCreated);
        }

        public static void OnLogEntryLogged(ILogger<JB2.Common.Enum.LogServerityType, string, ILogEntry> logger, ILogEntry entry)
        {
            if (LogEntryLogged != null)
                LogEntryLogged(logger, entry);
        }

        public static void OnExceptionOccured(Exception expection, ILogEntry entry)
        {
            if (ExceptionOccured != null)
                ExceptionOccured(expection, entry);
        }

        public static void OnApplicationInitilized(IApplication application, string authorizeKey)
        {
            if(ApplicationInitilized != null)
            {
                ApplicationInitilized(application, authorizeKey);
            }
        }

        public static void OnPlayerSignedIn(IBowtiePlayer player, IApplication application, DateTime signinDate)
        {
            if (PlayerSignedIn != null)
                PlayerSignedIn(player, application, signinDate);
        }
    }
}
