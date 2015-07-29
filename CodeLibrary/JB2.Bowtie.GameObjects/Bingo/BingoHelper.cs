using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public static class BingoHelper
    {
        public static IBingoCard GenerateNewBingoCard()
        {
            return GenerateNewBingoCard(Enum.BingoType.Standard);
        }

        public static BingoCell<byte>[] GenerateBingoCallList()
        {
            return GenerateBingoCallList(Enum.BingoType.Standard);
        }
        public static BingoCell<byte>[] GenerateBingoCallList(Enum.BingoType type)
        {
            List<BingoCell<byte>> result = new List<BingoCell<byte>>();

            switch (type)
            {
                case Enum.BingoType.Standard:
                    var bingoBalls = JB2.Common.Utility.RandomSubsetOfRange(1, 75, 75);
                    foreach (var b in bingoBalls)
                    {
                        string label = string.Empty;
                        if (b <= 15)
                            label = "B";
                        else if (b <= 30)
                            label = "I";
                        else if (b <= 45)
                            label = "N";
                        else if (b <= 50)
                            label = "G";
                        else if (b <= 75)
                            label = "O";

                        label = label + '-' + b.ToString();
                        result.Add(new BingoCell<byte>() { Label = label, Value = (byte)b });
                    }
                    break;
            }
                    
            return result.ToArray();
        }

        public static IBingoCard GenerateNewBingoCard(Enum.BingoType type)
        {
            StandardBingoCard result = new StandardBingoCard(type);

            switch(result.CardSize)
            {
                case Enum.BingoCardSize.s5:                   
                    for(int i=0;i < 5;i++)
                    {
                        IEnumerable<int> values = JB2.Common.Utility.RandomSubsetOfRange((15*i)+1, (15*i)+15, 5);
                        int cindex = 0;
                        foreach (int v in values)
                        {
                            result.Cells[i,cindex] = (byte)v;
                            cindex++;
                        }
                    }
                    break;
            }

            return result;

        }

    }
}
