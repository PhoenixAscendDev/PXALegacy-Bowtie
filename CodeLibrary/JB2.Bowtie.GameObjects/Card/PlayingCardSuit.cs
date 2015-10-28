using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie.GameObjects
{
    public struct PlayingCardSuit
    {

        #region Fields

        private JB2.Bowtie.Enum.PlayingCardSuitType _suit;

        #endregion Fields

        #region Constructors

        public PlayingCardSuit(Enum.PlayingCardSuitType type)
        {
            _suit = type;
        }

        #endregion Constructors

        #region Properties

        public Enum.PlayingCardSuitType SuitType
        {
            get
            {
                return _suit;
            }
        }

        public JB2.Common.JB2Color  Color
        {
            get
            {
                string hexValue = JB2.Common.Utility.GetAttributeOfType<JB2.Common.Attributes.ColorHex>(_suit).Hex;
                return JB2.Common.JB2Color.FromHex(hexValue);
            }
        }

        public bool isRed
        {
            get
            {
                return this.Color.HexString.ToUpper() == "FF0000";
            }
        }

        public bool isBlack
        {
            get
            {
                return this.Color.HexString.ToUpper() == "000000";
            }
        }

        #endregion Properties

        #region Implicit Operators

        public static implicit operator Enum.PlayingCardSuitType(PlayingCardSuit suit)
        {
            return suit.SuitType;
        }

        public static implicit operator PlayingCardSuit(Enum.PlayingCardSuitType type)
        {
            return new PlayingCardSuit(type);
        }

        public static implicit operator string(PlayingCardSuit suit)
        {
            return suit.SuitType.ToString();
        }

        public static bool operator ==(PlayingCardSuit x, PlayingCardSuit y)
        {
            if ((object)x == null) return (object)y == null;
            return x.SuitType == x.SuitType;
        }

        public static bool operator !=(PlayingCardSuit x, PlayingCardSuit y)
        {
            return !(x == y);
        }


        #endregion Implicit Operators

        #region Static 

        public static PlayingCardSuit Heart
        {
            get
            {
                return new PlayingCardSuit(Enum.PlayingCardSuitType.Heart);
            }
        }

        public static PlayingCardSuit Diamond
        {
            get
            {
                return new PlayingCardSuit(Enum.PlayingCardSuitType.Diamond);
            }
            
        }

        public static PlayingCardSuit Spade
        {
            get
            {
                return new PlayingCardSuit(Enum.PlayingCardSuitType.Spade);
            }
            
        }

        public static PlayingCardSuit Club
        {
            get
            {
                return new PlayingCardSuit(Enum.PlayingCardSuitType.Club);
            }
           
        }

        #endregion Static New

    }
}
