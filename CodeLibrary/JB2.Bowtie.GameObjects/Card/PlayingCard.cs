using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie.GameObjects
{
    public class PlayingCard : ICard<string, PlayingCardSuit>
    {
        #region Fields

        protected Enum.PlayingCardFaceType _value;
        protected PlayingCardSuit _suit;
        protected string _deckID;

        #endregion Fields

        #region Constructors

        public PlayingCard(Enum.PlayingCardFaceType valueType, Enum.PlayingCardSuitType suitType)
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

        public PlayingCardSuit Category
        {
            get
            {
                return _suit;
            }
        }

        public string Code
        {
            get
            {
                if (_value == PlayingCardFaceType.Joker)
                    return PlayingCardFaceType.Joker.ToString();
                else             
                    return _value.ToString() + " of " + _suit.ToString();
            }
        }

        public string FromDeckId
        {
            get
            {
                return _deckID;
            }
            set
            {
                _deckID = value;
            }
        }

        #endregion Properties

        #region IComparable

        public int CompareTo(ICard<string, PlayingCardSuit> other)
        {
            if (other == null) return 1;

            return this.Code.CompareTo(other.Code);
        }

        #endregion IComparable
    }
}
