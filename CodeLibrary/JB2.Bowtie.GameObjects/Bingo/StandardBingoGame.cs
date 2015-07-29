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
        private BingoBall<byte>[] _callOrder;
        private BingoBall<byte>[] _called;
        private string _id;
        private string _name;
        private BingoPatternType _pattern;

        public StandardBingoGame(Enum.BingoType type):this(type,JB2.Bowtie.Utility.GenerateNewObjectID())
        {

        }

        public StandardBingoGame(Enum.BingoType type,string id)
        {
            this._type = type;
            this._callOrder = JB2.Bowtie.GameObjects.BingoHelper.GenerateBingoCallList(type);
            this._id = id;
            this._name = string.Empty;
            //this._callOrder = new List<BingoBall<byte>>().ToArray();
            this._called = new List<BingoBall<byte>>().ToArray();
            this._pattern = new BingoPatternType();

            base.serializableProperties = new List<string>();

            foreach (System.Reflection.PropertyInfo p in this.GetType().GetProperties())
            {
                base.serializableProperties.Add(p.Name);
            }
        }


        public BingoBall<byte>[] CallOrder
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

        public BingoBall<byte>[] PreviousCalls
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
                return 0;
                //return (byte)_called.Length;
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
