using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie;

namespace JB2.Events
{
    public class Bowtie : JB2.Common.Singleton<Bowtie>
    {

        public event Action<ILogger<JB2.Common.Enum.LogServerityType, string, ILogEntry>, ILogEntry> LogEntryLogged;

        public event Action<JB2.Bowtie.DewdropData, JB2.Bowtie.IDewdropEntry> DewdropDataUpdated;


        public static void OnLogEntryLogged(ILogger<JB2.Common.Enum.LogServerityType, string, ILogEntry> logger, ILogEntry entry)
        {
            if (JB2.Events.Bowtie.Instance.LogEntryLogged != null)
                JB2.Events.Bowtie.Instance.LogEntryLogged(logger, entry);
        }

        public static void OnDewdropDataUpdated(DewdropData data, IDewdropEntry entry)
        {
            if (JB2.Events.Bowtie.Instance.DewdropDataUpdated != null)
                JB2.Events.Bowtie.Instance.DewdropDataUpdated(data, entry);
        }



    }
}
