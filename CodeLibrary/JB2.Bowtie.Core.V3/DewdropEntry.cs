using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class DewdropEntry : IDewdropEntry
    {
        public string PlayerID { get; set; }
        public int Value { get; set; }
        public string GDID { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string ApplicationID { get; set; }
    }
}
