using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IGraphElement : JB2.Common.IIDNamePair<string,string>,JB2.Common.IMetaData, JB2.Common.IMetaDatable
    {
        string ParentID { get; set; }
        string ApplicationID { get; set; }

        GraphElementType ElementType { get; set; }

    }
}
