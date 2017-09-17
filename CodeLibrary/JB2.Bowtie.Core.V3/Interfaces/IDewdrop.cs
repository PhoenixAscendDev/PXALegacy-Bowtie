using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public interface IDewdrop : IIDNamePair<string,string>
    {
        string ApplicationID { get; set; }
        string GDID { get; set; }
        string ParentGDID { get; set; }
        bool IsActive { get; set; }

        Enum.DewDropValueType ValueType { get; set; }
    }
}
