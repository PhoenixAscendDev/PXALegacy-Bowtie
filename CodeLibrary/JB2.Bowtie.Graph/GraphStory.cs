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
    }
}
