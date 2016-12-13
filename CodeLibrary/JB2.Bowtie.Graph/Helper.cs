using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Helper
{
    public static class Graph
    {
        public static string GenerateID<TElement>()
            where TElement : IGraphElement, new()
        {

            TElement entity = new TElement();

            var url = JB2.Configuration.GetAppSetting("JB2:UrlHash:BowtieGraphNewID");
            int rng = JB2.Helper.Bowtie.NewRNG();
            long count = JB2.Infrastructure.Counter.GetNext("Graph" + entity.ElementType.ToString());
            url = String.Format(url, entity.ElementType.ToString(), count.ToString("D6"), rng.ToString());
            StringBuilder id = new StringBuilder();
            switch(entity.ElementType)
            {
                case JB2.Bowtie.Enum.GraphElementType.Action:
                    id.Append("a");
                    break;
                case JB2.Bowtie.Enum.GraphElementType.Object:
                    id.Append("o");
                    break;
                case JB2.Bowtie.Enum.GraphElementType.Property:
                    id.Append("p");
                    break;
                case JB2.Bowtie.Enum.GraphElementType.Story:
                    id.Append("s");
                    break;                
            }

            //id.Append(rng.ToString());
            //id.Append("-");
            //id.Append(count.ToString("D6"));
            //id.Append("-");
            id.Append(JB2.Common.NewID.UriHash(new Uri(url)).ToUpper());
            return id.ToString();
        }

    }
}
