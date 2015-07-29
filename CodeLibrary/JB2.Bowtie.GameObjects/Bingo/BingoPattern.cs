using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public class BingoPatternType : JB2.Common.IIDNamePair<string,string>
    {
        
        private string _id;
        private string _name;
        private Enum.BingoType _type;
        private string[] _victorys;

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
    }
}
