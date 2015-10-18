using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public class BingoCardStyle : JB2.Common.IIDNamePair<string,string>
    {

        public string ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
        public Enum.BingoCardSize CardSize
        {
            get; set;
        }

        public int[] SpaceFontLocationX
        {
            get; set;
        }

        public int[] SpaceFontLocationY
        {
            get; set;
        }

        public string SpaceFontCode
        {
            get; set;
        }

        public string SpaceFontSize
        {
            get; set;
        }

        public string SpaceFontColor
        {
            get; set;
        }





        
    }
}
