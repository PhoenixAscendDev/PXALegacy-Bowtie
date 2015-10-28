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

        protected Enum.PlayingCardValueType _value;
        protected PlayingCardSuit _suit;
        protected string _deckID;

        #endregion Fields

        #region Constructors

        public PlayingCard(Enum.PlayingCardValueType valueType, Enum.PlayingCardSuitType suitType)
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

        public int CompareTo(ICard<string, PlayingCardSuit> other)
        {
            return 1;
        }
    }
}
