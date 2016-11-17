using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Grab;

namespace JB2.Bowtie
{ 
    public static class GraphExtension
    {

        public static JB2.Grab.IEdge ToEdge(this JB2.Bowtie.GraphAction action)
        {
            JB2.Grab.Edge edge = Edge.New(action.Name);

            foreach (var p in action.GetProperties())
            {
                edge.AddProperty<string>(p.Name, string.Empty);
            }
            return edge;
        }

        public static JB2.Grab.INode ToNode(this JB2.Common.IClass o)
        {
            INode node = null;
            try
            {
                GraphElementAttribute idAttribute = ((GraphElementAttribute)Attribute.GetCustomAttribute(o.GetType(), typeof(GraphElementAttribute)));
                string id = idAttribute.ID;

                var graphRepo = JB2.Settings.Bowtie.UnitOfWork.GraphRepository;

                var graphObject = graphRepo.GetGraphElement(id);

                node = Node.New(graphObject.Name);

                //now we have the node, lets set the node properties

                var attributes = Attribute.GetCustomAttributes(o.GetType(), typeof(GraphPropertyMapAttribute));

                foreach (var attribute in attributes)
                {
                    var a = (GraphPropertyMapAttribute)attribute;
                    try
                    {
                        object value = null;
                        switch (a.MemberType)
                        {
                            case Enum.ClassMemberType.Property:
                                value = o.GetType().GetProperty(a.MemberName).GetValue(o, null);
                                break;
                            case Enum.ClassMemberType.Method:
                                value = o.GetType().GetMethod(a.MemberName).Invoke(o, null);
                                break;
                        }

                        if(value != null)
                            node.AddProperty<string>(a.GraphPropertyName, value.ToString());

                    }
                    catch (Exception ex)
                    {
                        ex.BowtieLog();
                    }
                }

                //now that we fill in the object property, we should make sure al defined graph properties is added
                foreach (var p in ((GraphObject)graphObject).GetProperties())
                {
                    var prop = node.GetProperties().ToList().Find(x => x.PropertyName == p.PropertyName);

                    if(prop == null)
                    {
                        node.AddProperty<string>(p.PropertyName, string.Empty);
                    }
                }


            }
            catch (Exception ex)
            {
                ex.BowtieLog();
            }

            return node;
        }

        public static IPlayerStory ToPlayerStory(this JB2.Common.IClass o, string playerID, string applicationID, string actionName, IEnumerable<JB2.Common.IMetaData> playerData = null)
        {
            try
            {
                GraphElementAttribute idAttribute = ((GraphElementAttribute)Attribute.GetCustomAttribute(o.GetType(), typeof(GraphElementAttribute)));
                string id = idAttribute.ID;

                var graphRepo = JB2.Settings.Bowtie.UnitOfWork.GraphRepository;

                var graphObject = (GraphObject)graphRepo.GetGraphElement(id);
                var graphAction = (GraphAction)graphRepo.GetGraphElementByName(actionName);

                var node = o.ToNode();
                var edge = graphAction.ToEdge();

                PlayerStory pStory = new PlayerStory(playerID, applicationID, (GraphAction)graphAction, (GraphObject)graphObject);
                pStory.ObjectData = node.GetProperties();
                pStory.ActionData = edge.GetProperties();

                pStory.ObjectData.ToList().Add(new JB2.Common.MetaData<string>("objectID", graphObject.ID));
                pStory.ActionData.ToList().Add(new JB2.Common.MetaData<string>("actionID", graphAction.ID));

                if (playerData != null)
                    pStory.PlayerData = playerData;
                return pStory;
            }

            catch (Exception ex)
            {
                ex.BowtieLog();
                return null;
            }
        }

    }
}
