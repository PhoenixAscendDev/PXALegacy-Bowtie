using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie.GameObjects
{
    public class StandardPlayingCard : IPlayingCard<Enum.PlayingCardValueType, PlayingCardSuit>
    {

        #region Fields

        protected Enum.PlayingCardValueType _value;
        protected PlayingCardSuit _suit;

        #endregion Fields

        #region Constructors

        public StandardPlayingCard(Enum.PlayingCardValueType valueType, Enum.PlayingCardSuitType suitType)
        {
            _value = valueType;
            _suit = suitType;
        }

        #endregion Constructors


        #region Properties
        public string Label
        {
            get
            {
                return string.Empty;
            }
        }

        public PlayingCardSuit Suit
        {
            get
            {
                return _suit;
            }
        }

        public PlayingCardValueType Value
        {
            get
            {
                return _value;
            }
        }

        #endregion Properties

        public int CompareTo(IPlayingCard<PlayingCardValueType, PlayingCardSuit> other)
        {
            return 1;
        }
    }
}
