using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

using JB2.Bowtie.GameObjects;
using JB2.Common.Extensions;

namespace JB2.Bowtie.GameObjects
{

    public class BingoBallDeck :  BingoBallDeck<byte>
    {
        public BingoBallDeck(string id, BingoBall<byte>[] balls) : base(id,balls)
        {

        }

    }


    public class BingoBallDeck<Tnum> : Deck<BingoBall<Tnum>>, IBingoBalDeck<Tnum>
    {


        #region Constructors

        

        public BingoBallDeck(string id, BingoBall<Tnum>[] balls) : base(id,balls)
        {

        }

        private BingoBallDeck(BingoBall<Tnum>[] balls) : base(null,balls)
        {

        }

        #endregion Constructors


        

        #region Static Methods

        public static BingoBallDeck<byte> NewBingoBallDeck(Enum.BingoType bingoType)
        {
            BingoBall<byte>[] balls = BingoHelper.GenerateBingoCallList(bingoType);
            return new BingoBallDeck<byte>(balls);
        }

        #endregion

    }
}
