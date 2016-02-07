using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public class GraphProperty : GraphElement,IGraphElement
    {
        Enum.GraphPropertyType GraphPropertyType { get; set; }
        bool isMultiValued { get; set; }
        public override GraphElementType ElementType
        {
            get
            {
                return GraphElementType.Property;
            }
        }
    }
}
