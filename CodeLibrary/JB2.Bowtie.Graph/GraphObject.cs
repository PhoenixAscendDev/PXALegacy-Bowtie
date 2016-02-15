using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public class GraphObject : GraphElement, IGraphElement
    {
        #region Fields
        protected JB2.Common.BaseCollection<GraphProperty> _properties;
        #endregion Fields

        #region Constructor
        public GraphObject()
        {
            _properties = new Common.BaseCollection<GraphProperty>();
        }

        #endregion Constructor
        public IEnumerable<GraphProperty> GetProperties()
        {
            return _properties;
        }

        public JB2.Common.ServiceResult AddProperty(GraphProperty p)
        {
            return _properties.Add(p);
        }

        public JB2.Common.ServiceResult RemoveProperty(GraphProperty p)
        {
            return _properties.Remove(p);
        }

        string Singular { get; set; }
        string Plural { get; set; }

        public Enum.GraphDeterminer Determiner
        {
            get;
            set;
        }

        public override GraphElementType ElementType
        {
            get
            {
                return GraphElementType.Object;
            }
        }

        public static GraphObject NewObject(string name, string applicationid, string determiner, string pural)
        {
            var result = new GraphObject();
            result.Name = name;
            result.ID = "o_" + JB2.Common.NewID.ShortGuid();
            result.ApplicationID = applicationid;
            result.Singular = name;
            result.Plural = pural;

            return result;
                
        }
    }
}
