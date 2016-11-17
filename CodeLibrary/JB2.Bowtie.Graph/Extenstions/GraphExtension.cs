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

                foreach (var a in attributes)
                {
                    try
                    {
                        var value = o.GetType().GetProperty(((GraphPropertyMapAttribute)a).MemberName).GetValue(o, null);
                        node.AddProperty<string>(((GraphPropertyMapAttribute)a).GraphPropertyName, value.ToString());
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

        public static IPlayerStory ToPlayerStory(this JB2.Common.IClass o, string playerID, string applicationID, string actionName)
        {
            try
            {
                GraphElementAttribute idAttribute = ((GraphElementAttribute)Attribute.GetCustomAttribute(o.GetType(), typeof(GraphElementAttribute)));
                string id = idAttribute.ID;

                var graphRepo = JB2.Settings.Bowtie.UnitOfWork.GraphRepository;

                var graphObject = graphRepo.GetGraphElement(id);



                PlayerStory pStory = new PlayerStory(playerID, applicationID, (GraphStory)graphObject);

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
