using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public class StandardBingoCard : JB2.API.BaseObject,IBingoCard
    {
        private Enum.BingoType _type;
        private byte[,] _cells;
        private byte[,] _marks;
        private string _id;
        private string _name;
        private Enum.BingoCardSize _gridsize;

        #region Public Properies

        public StandardBingoCard() : 
            this(Enum.BingoType.Standard, JB2.Common.Utility.GenerateKey(Common.Enum.KeyBitSize.keybit64,Common.Utility.RandomString(8,true)))
        {

        }

        public StandardBingoCard(Enum.BingoType type) :
            this(type, JB2.Common.Utility.GenerateKey(Common.Enum.KeyBitSize.keybit64, Common.Utility.RandomString(8, true)))
        {

        }

        public StandardBingoCard(Enum.BingoType type, string id)
        {
            _id = id;
            _type = type;
            _name = string.Empty;
            switch(_type)
            {
                case  Enum.BingoType.Standard:
                    _gridsize = Enum.BingoCardSize.s5;
                    break;
            }
            initGrid();
            base.serializableProperties = new List<string>();

            foreach (System.Reflection.PropertyInfo p in this.GetType().GetProperties())
            {
                base.serializableProperties.Add(p.Name);
            }
        }

        public Enum.BingoType BingoType
        {
            get
            {
                return _type;
            }
            
        }

        public byte[,] Cells
        {
            get
            {
                return _cells;
            }
            set
            {
                _cells = value;
            }
        }

        public byte[,] CellMarks
        {
            get
            {
                return _marks;
            }
            set
            {
                _marks = value;
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

        #endregion Public Properies

        private void initGrid()
        {
            
            switch(this._gridsize)
            {
                case Enum.BingoCardSize.s5:

                    _cells = new byte[5, 5];
                    _marks = new byte[5, 5];
                    for(int i=0;i < 5;i++)
                    {
                        for(int j=0;j < 5; j++)
                        {
                            _cells[i,j] = 0;
                            _marks[i,j] = 0;
                        }
                    }
                    break;
            }

        }


        public Enum.BingoCardSize CardSize
        {
            get { return _gridsize;}
        }
    }
}
