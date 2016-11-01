using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class GraphStory: GraphElement
    {
        public GraphAction AssociatedAction { get; set; }
        public GraphObject AssociatedObject { get; set; }

        public JB2.Common.WordTense ActionTense { get; set; }

        public override Enum.GraphElementType ElementType
        {
            get
            {
                return Enum.GraphElementType.Story;
            }
        }


        public static GraphStory NewStory(string name, string applicationID)
        {
            var result = new GraphStory();
            result.Name = name;          
            result.ID = "s_" + JB2.Common.NewID.ShortGuid();
            result.ApplicationID = applicationID;

            JB2.Common.WordTense tenses = new Common.WordTense();

            result.ActionTense = tenses;
            result.AssociatedAction = new GraphAction();
            result.AssociatedObject = new GraphObject();

            result.ParentID = string.Empty;

            return result;

        }
    }
}
