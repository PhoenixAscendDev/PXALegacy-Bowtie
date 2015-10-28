using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public class PlayingCardDeck : Deck<PlayingCard>
    {

        public PlayingCardDeck( PlayingCard[] cards) : this(null,cards)
        {

        }

        public PlayingCardDeck( string id, PlayingCard[] cards) : base(id,cards)
        {
        }
    }
}
