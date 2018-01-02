using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IPointEntry : IPlayerable<string>, IApplicationable<string>
    {
        string ID { get; set; }
        DateTime SubmittedDate { get; set; }
        string ApplicationID { get; set; }

        string PlayerID { get; set; }

        int PointsEarned { get; set; }

        string ReferenceCode { get; set; }


    }
}
