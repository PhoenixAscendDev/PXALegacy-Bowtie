using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

using JB2.Common.Extensions;

namespace JB2.Bowtie.GameObjects
{
    public static class BingoHelper
    {
        public static IBingoCard GenerateNewBingoCard()
        {
            return GenerateNewBingoCard(Enum.BingoType.Standard);
        }

        public static BingoBall<byte>[] GenerateBingoCallList()
        {
            return GenerateBingoCallList(Enum.BingoType.Standard);
        }
        public static BingoBall<byte>[] GenerateBingoCallList(Enum.BingoType type)
        {
            List<BingoBall<byte>> result = new List<BingoBall<byte>>();

            switch (type)
            {
                case Enum.BingoType.Standard:
                    List<int> bingoBalls = new List<int>();
                    for (int i = 1; i <= 75;i++ )
                    {
                        result.Add((BingoBall)i);
                    }
                    result.Shuffle();
                    
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

                        IList<int> list = values.ToList();

                        list.Shuffle();
                        int cindex = 0;
                        foreach (int v in list)
                        {
                            result.Cells[i,cindex] = (byte)v;
                            cindex++;
                        }
                    }
                    break;
            }

            return result;

        }

        public static JB2Image GetBingoCardImage(IBingoCard cardData)
        {
            return new JB2Image();
        }

        public static JB2Image GetBingoCardImage(string cardID, string cardBGCode )
        {
            return new JB2Image();
        }

        public static string CalculateChecksum(byte[,] values)
        {
            string checksumFormat = "{0}>*<{1}>*<{2}>*<{3}>*<{4}";
            int numberOfRows = values.GetLength(0);
            int numberOfColumns = values.GetLength(1);

            long totalSum = 0;
            long topSum = 0;
            long bottomSum = 0;

            for(int i = 0; i < numberOfRows; i++)
            {
                for(int j = 0; j < numberOfColumns; j++)
                {
                    totalSum = totalSum + values[i, j];
                    if (i == 0)
                        topSum = topSum + values[i, j];
                    if (i == numberOfRows -1 )
                        bottomSum  = bottomSum + values[i, j];
                }
            }

            return string.Format(checksumFormat, (totalSum * 5).ToString(), (topSum * 6).ToString(), (bottomSum * 6).ToString(), values[0, 0], values[numberOfRows - 1, numberOfColumns - 1]);

        }


    }
}
