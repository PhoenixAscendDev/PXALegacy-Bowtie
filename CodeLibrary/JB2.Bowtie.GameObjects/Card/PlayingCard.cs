using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie.GameObjects
{
    public class PlayingCard : BowtieObject,ICard<string, PlayingCardSuit>, IDeckable<PlayingCard>
    {
        #region Fields

        protected Enum.PlayingCardFaceType _face;
        protected PlayingCardSuit _suit;
        protected string _deckID;
        protected int _value;

        #endregion Fields

        #region Constructors

        public PlayingCard(Enum.PlayingCardFaceType faceType, Enum.PlayingCardSuitType suitType, bool isAceHigh) : base(BowtieObjectType.bowtie_gameobject,null)
        {
            _face = faceType;
            _suit = suitType;
            switch(faceType)
            {
                case PlayingCardFaceType.Ace:
                    _value = isAceHigh ? 14 : (int)faceType;
                    break;
                default:
                    _value = (int)faceType;
                    break;     
            }
        }

        #endregion Constructors

        #region Properties

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }

        }

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
                if (_face == PlayingCardFaceType.Joker)
                    return PlayingCardFaceType.Joker.ToString();
                else             
                    return _face.ToString() + "-" + _suit.ToString();
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

        public int CompareTo(PlayingCard other)
        {
            if (other == null) return 1;

            return this.Code.CompareTo(other.Code);
        }

        #endregion IComparable

        #region ToString()

        public override string ToString()
        {
            return this.Code;
        }

        #endregion ToString()

        #region Equals()

        public override bool Equals(object obj)
        {
            return obj is PlayingCard ? ((PlayingCard)obj).Code == Code : false;
        }

        #endregion Equals

        #region GetHasCode()

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        #endregion




    }
}
