using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{

    public interface IBingoBallDeck : IBingoBalDeck<byte>
    {

    }

    public interface IBingoBalDeck<Tnum> : IDeck<BingoBall<Tnum>>
    {
        
    }
}
