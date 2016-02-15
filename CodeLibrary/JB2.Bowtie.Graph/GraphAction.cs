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
        protected JB2.Common.BaseCollection<GraphObject> _objects;
        #endregion Fields

        public GraphAction()
        {
            _properties = new Common.BaseCollection<GraphProperty>();
            _objects = new Common.BaseCollection<GraphObject>();
        }
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

        public JB2.Common.ServiceResult AddObject(GraphObject obj)
        {
            return _objects.Add(obj);
        }

        public JB2.Common.ServiceResult RemoveObject(GraphObject obj)
        {
            return _objects.Remove(obj);
        }



        public IEnumerable<GraphObject> GetAssociatedObjects()
        {
            return _objects;
        }

        public static GraphAction NewAction(string name, string applicationid)
        {
            GraphAction act = new GraphAction();
            act.ID = "a_" + JB2.Common.NewID.ShortGuid();
            act.Name = name;
            act.ApplicationID = applicationid;

            return act;
        }

    }
}
