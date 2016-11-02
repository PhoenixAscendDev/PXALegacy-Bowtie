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

        public GraphProperty() : base()
        {

        }

        public JB2.Common.IDNamePair GraphPropertyType { get; set; }
        public bool isMultiValued { get; set; }
        public override GraphElementType ElementType
        {
            get
            {
                return GraphElementType.Property;
            }
        }

        public static GraphProperty NewProperty(string name, JB2.Common.IDNamePair type,string applicationid,bool isMultivalued)
        {
            GraphProperty p = new GraphProperty();
            p.ID = "p_" + JB2.Common.NewID.ShortGuid();
            p.Name = name;
            p.GraphPropertyType = type;
            p.ApplicationID = applicationid;
            p.isMultiValued = isMultivalued;
            return p;
        }
    }
}
