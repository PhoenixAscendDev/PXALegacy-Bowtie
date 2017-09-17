using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IDewdropEntry 
    {
        string PlayerID { get; set; }
        int Value { get; set; }
        string GDID { get; set; }

        DateTime SubmittedDate { get; set; }

        string ApplicationID { get; set; }



    }
}
