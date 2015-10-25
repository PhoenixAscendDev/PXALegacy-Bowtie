using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Extensions;

namespace JB2.Bowtie.GameObjects
{
    public class BingoPatternType : JB2.Common.IIDNamePair<string,string>
    {
        #region Fields

        private string _id;
        private string _name;
        private Enum.BingoType _type;
        private string[] _victorys;

        #endregion Fields

        #region Properties
        Enum.BingoType BingType
        {
            get
            {
                return _type;
            }
        }

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
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

        public string[] WinningPatterns
        {
            get
            {
                return _victorys;
            }
            set
            {
                _victorys = value;
            }
            
        }

        #endregion Properties

        #region Constructors

        public BingoPatternType()
        {

        }

        #endregion Constructors

        #region Operators

        public static implicit operator System.Collections.BitArray[] (BingoPatternType bp)
        {
            List<System.Collections.BitArray> list = new List<System.Collections.BitArray>();

            foreach(string s in bp.WinningPatterns)
            {
                list.Add(s.ToBitArray());
            }


            return list.ToArray();
        }

        #endregion Operators

        #region Convert To

        public System.Collections.BitArray[] ToBitArray()
        {
            return (System.Collections.BitArray[])this;
        }

        #endregion Convert To


    }
}
