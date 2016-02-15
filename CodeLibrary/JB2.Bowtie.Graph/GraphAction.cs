using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public class GraphAction : GraphElement,IGraphElement
    {
        #region Fields
        protected JB2.Common.BaseCollection<GraphProperty> _properties;
        
        #endregion Fields
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

        public override GraphElementType ElementType
        {
            get
            {
                return GraphElementType.Action;
            }
        }

        public IEnumerable<GraphObject> GetAssociatedObject()
        {
            throw new NotImplementedException();
        }

    }
}
