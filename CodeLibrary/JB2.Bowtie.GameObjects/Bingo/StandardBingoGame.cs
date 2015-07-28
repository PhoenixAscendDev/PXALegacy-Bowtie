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
        private List<byte> _callOrder;
        private List<byte> _called;
        private string _id;
        private string _name;
        private BingoPatternType _pattern;

        public StandardBingoGame(Enum.BingoType type)
        {
            this._type = type;
            this._callOrder = JB2.Bowtie.GameObjects.BingoHelper.GenerateBingoCallList(type).ToList();
        }

        public byte[] CallOrder
        {
            get
            {
                return _callOrder.ToArray();
            }
            set
            {
                _callOrder = value.ToList();
            }
            
        }

        public byte[] PreviousCalls
        {
            get
            {
                return _called.ToArray();
            }
            set
            {
                _called = value.ToList();
            }
        }

        public byte CallCount
        {
            get
            {
                return (byte)_called.Count;
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
