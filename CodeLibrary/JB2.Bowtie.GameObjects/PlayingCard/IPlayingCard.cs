using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public interface IPlayingCard<Tvalue,Tsuit> : IDeckable<IPlayingCard<Tvalue, Tsuit>>
    {
        Tvalue Value { get; }
        string Label { get; }
        Tsuit Suit { get; }
    }
}
