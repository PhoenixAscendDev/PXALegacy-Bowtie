using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IPointGiver : JB2.Common.IIDNamePair<string,string>
    {
        string PointGiverType { get; set; }

        string Description { get; set; }


    }
}
