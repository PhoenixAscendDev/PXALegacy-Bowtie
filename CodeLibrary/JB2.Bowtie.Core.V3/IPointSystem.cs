using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;



namespace JB2.Bowtie
{

    public interface IPointSystem : IPointSystem<string>
    {

    }

    public interface IPointSystem<TImage> : JB2.Common.IIDNamePair<string, string>
    {
        string Single { get; set; }
        string Plural { get; set; }

        JB2.Common.WordTense ReceiveTense { get; set; }

        TImage GetIcon(int point);


        

    }
}
