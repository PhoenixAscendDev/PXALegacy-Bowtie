using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;


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

        public static JB2Image GenerateBingoCardImage(IBingoCard cardData, JB2Image backgroundImage)
        {

            int[] locationX = new int[] { 25, 100, 168, 243, 315 };
            int[] locationY = new int[] { 90, 160, 228, 300, 369 };
            using (Graphics graphics = Graphics.FromImage(backgroundImage))
            {
                using (Font arialFont = JB2.Common.FontHelper.GetFont("ffft1", 40))
                {

                    for (int colIndex = 0; colIndex < 5; colIndex++)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            PointF blocation = new PointF(10f, 10f);
                            blocation = new PointF(locationX[colIndex], locationY[i]);
                            // blocation = new PointF( blocation.X , blocation.Y);
                            if (cardData.Cells[colIndex, i] > 10)
                                blocation = new PointF(blocation.X - 10, blocation.Y);
                            else if (cardData.Cells[colIndex, i] == 10)
                                blocation = new PointF(blocation.X - 5, blocation.Y);
                            //else
                            //    blocation = new PointF(8 + (70 * colIndex), (90 * (i + 1)));
                            string cellText = cardData.Cells[colIndex, i].ToString();

                            //center is a free space
                            if ((colIndex != 2) || (i != 2))
                                graphics.DrawString(cellText, arialFont, Brushes.Black, blocation);

                        }
                    }
                }
            }
            System.IO.MemoryStream imageStream = new System.IO.MemoryStream();

            ((System.Drawing.Image)backgroundImage).Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);

            byte[] imageBytes = backgroundImage.FileContent;

            return backgroundImage;

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
