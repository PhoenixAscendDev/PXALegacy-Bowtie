using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface ISystem : JB2.Common.IIDNamePair<string,string>
    {
        Enum.SystemType SystemType { get; set; }
    }
}
