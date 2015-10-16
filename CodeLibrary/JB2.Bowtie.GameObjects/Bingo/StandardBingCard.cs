using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie.GameObjects
{
    public class StandardBingoCard : BowtieObject, IBingoCard
    {
        private BingoType _type;
        private byte[,] _cells;
        private byte[,] _marks;
        private string _id;
        private string _name;
        private Enum.BingoCardSize _gridsize;
        private bool _freeCenter;

        #region Public Properies

        public StandardBingoCard() : 
            this(Enum.BingoType.Standard)
        {

        }

        public StandardBingoCard(Enum.BingoType type): this(type,true,"STA-" + JB2.Bowtie.Utility.GenerateNewObjectID())
        {

        }

        

        public StandardBingoCard(Enum.BingoType type,bool freeCenter,string id)
        {
            _freeCenter = freeCenter;
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
            //base.serializableProperties = new List<string>();

            //foreach (System.Reflection.PropertyInfo p in this.GetType().GetProperties())
            //{
            //    base.serializableProperties.Add(p.Name);
            //}
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
                _id = BingoHelper.GenerateID(this.BingoType, value);
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
                            _marks[i,j] = (i==2 && j==2 && this._freeCenter) ? (byte)1 : (byte)0;
                        }
                    }
                    break;
            }

        }

        public Enum.BingoCardSize CardSize
        {
            get { return _gridsize;}
        }

        public static implicit operator System.Collections.BitArray(StandardBingoCard card)
        {
            List<bool> marks = new List<bool>(25);
            for(int r=0;r < 5;r++)
            {
                for(int c=0;c < 5;c++)
                {
                    marks.Add(card.CellMarks[r, c] >= 1 ? true : false);
                }
            }
            return new System.Collections.BitArray(marks.ToArray());
        }

        public System.Collections.BitArray ToBitArray()
        {
            return (System.Collections.BitArray)this;
        }
       
        public string MarkString
        {
            get
            {
                StringBuilder result = new StringBuilder();
                System.Collections.BitArray bitArray = this.GetMarks();
                for (int i = 0; i < bitArray.Count; i++)
                {
                    bool bit = bitArray.Get(i);
                    result.Append(bit ? "1" : "0");
                    
                }
                return result.ToString();
            }
        }

        public string UniqueToken
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public BowtieObjectType Kind
        {
            get
            {
                return BowtieObjectType.bowtie_gameobject;
            }
        }

        public ObjectTag[] Tags
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public GameObjectType GameObjectType
        {
            get
            {
                return GameObjectType.BingoCard;
            }
        }

        public System.Collections.BitArray GetMarks()
        {
            return this.ToBitArray();
        }

        public bool AddTag(ObjectTag tag)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTag(ObjectTag tag)
        {
            throw new NotImplementedException();
        }
    }
}
