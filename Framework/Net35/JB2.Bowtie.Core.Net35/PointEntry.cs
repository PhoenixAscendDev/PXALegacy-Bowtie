using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public class PointEntry : IPointEntry
    {
        public string ID { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string ApplicationID { get; set; }
        public string PlayerID { get; set; }
        public int PointsEarned { get; set; }
        public string ReferenceCode { get; set; }

        public string GetApplicationID()
        {
            return ApplicationID;
        }

        public string GetPlayerID()
        {
            return PlayerID;
        }


        public static PointEntry New
        {
            get
            {
                var r = new PointEntry();
                r.ID = JB2.Common.NewID.Guid();

                return r;
            }
        }
    }
}
