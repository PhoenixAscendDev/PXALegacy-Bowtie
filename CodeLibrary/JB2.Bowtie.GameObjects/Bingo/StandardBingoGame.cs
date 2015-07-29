using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public class StandardBingoGame : JB2.API.BaseObject,IBingoGame<byte>
    {
        private Enum.BingoType _type;
        private BingoCell<byte>[] _callOrder;
        private BingoCell<byte>[] _called;
        private string _id;
        private string _name;
        private BingoPatternType _pattern;

        public StandardBingoGame(Enum.BingoType type)
        {
            this._type = type;
            this._callOrder = JB2.Bowtie.GameObjects.BingoHelper.GenerateBingoCallList(type);
        }

        public BingoCell<byte>[] CallOrder
        {
            get
            {
                return _callOrder;
            }
            set
            {
                _callOrder = value;
            }
            
        }

        public BingoCell<byte>[] PreviousCalls
        {
            get
            {
                return _called;
            }
            set
            {
                _called = value;
            }
        }

        public byte CallCount
        {
            get
            {
                return (byte)_called.Length;
            }
        }

        public Enum.BingoType BingoType
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }

        }

        public BingoPatternType PatternType
        {
            get
            {
                return _pattern;
            }
            set
            {
                _pattern = value;
            }
        }

        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}
