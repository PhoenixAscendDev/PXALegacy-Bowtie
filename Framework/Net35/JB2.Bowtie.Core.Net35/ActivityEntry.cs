using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public class ActivityEntry : Bowtie.BowtieObject, IActivityEntry
    {

        public string ApplicationID { get; set; }

        public string PlayerID { get; set; }
        public DateTime ActivityDate { get; set; }
        public ActivityDataset Data { get; set; }
        public string ActivityCode { get; set; }

        public string GetApplicationID()
        {
            return ApplicationID;
        }

        public string GetPlayerID()
        {
            return PlayerID;
        }

        public static ActivityEntry New
        {
            get
            {
                var r = new ActivityEntry();
                r.ID = JB2.Common.NewID.Guid();
                return r;
            }
        }
    }
}
