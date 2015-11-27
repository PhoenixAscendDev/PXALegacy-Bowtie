using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public struct BingoCardConfig
    {
        public int[] LocationX { get; set; }
        public int[] LocationY { get; set; }
        public System.Drawing.Font Font { get; set; }
        public JB2.Common.JB2Image BackgroundImage { get; set; }

        

    }
}
