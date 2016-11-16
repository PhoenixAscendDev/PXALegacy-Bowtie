using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{ 
    public static class GraphExtension
    {
        public static IPlayerStory ToPlayerStory(this JB2.Common.IClass o, string playerID)
        {
            try
            {
                GraphStoryAttribute idAttribute = ((GraphStoryAttribute)Attribute.GetCustomAttribute(o.GetType(), typeof(GraphStoryAttribute)));
                string id = idAttribute.ID;

                var graphRepo = JB2.Settings.Bowtie.UnitOfWork.GraphRepository;

                var graphStory = graphRepo.GetGraphElement(id);

                PlayerStory pStory = new PlayerStory(playerID, (GraphStory)graphStory);

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
