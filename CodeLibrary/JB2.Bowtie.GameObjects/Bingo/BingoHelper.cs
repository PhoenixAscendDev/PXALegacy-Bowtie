using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

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
                    byte[,] spaces = new byte[5, 5];
                    for (int i=0;i < 5;i++)
                    {
                        IEnumerable<int> values = JB2.Common.Utility.RandomSubsetOfRange((15*i)+1, (15*i)+15, 5);

                        IList<int> list = values.ToList();

                        list.Shuffle();
                        int cindex = 0;
                        foreach (int v in list)
                        {
                            spaces[i,cindex] = (byte)v;
                            cindex++;
                        }
                    }
                    result.Cells = spaces;
                    break;
            }

            return result;

        }

        public static JB2Image GenerateBingoCardImage(IBingoCard cardData, JB2Image backgroundImage)
        {
            return GenerateBingoCardImage(cardData, "bingoforever", backgroundImage);
        }

        public static System.Collections.BitArray ConvertToBitArray(IBingoCard card)
        {

            List<bool> marks;
            int maxRows = 0;
            int maxColumns = 0;

            switch (card.CardSize)
            {
                case JB2.Bowtie.Enum.BingoCardSize.s5:
                default:
                    marks = new List<bool>(25);
                    maxRows = 5;
                    maxColumns = 5;
                    break;
            }



            for (int r = 0; r < maxRows; r++)
            {
                for (int c = 0; c < maxColumns; c++)
                {
                    marks.Add(card.CellMarks[r, c] >= 1 ? true : false);
                }
            }
            return new System.Collections.BitArray(marks.ToArray());

        }


        public static string GenerateID(IBingoCard card) 
        {
            return GenerateID(card.BingoType, card.Cells);

        }

        public static string GenerateID(Enum.BingoType type, byte[,] spaces)
        {
            return GenerateID(type, spaces, JB2.Bowtie.Utility.GenerateNewObjectID());
        }

        public static string GenerateID(Enum.BingoType type, byte[,] spaces,string guid)
        {
            string checksum = CalculateChecksum(spaces);
            string id = string.Empty;

            switch(type)
            {
                case Enum.BingoType.Standard:
                    id = "STA-";
                    break;
                default:
                    id = "BNG-";
                    break;
            }

            id = id + checksum.Replace(">*<", "-");
            id = id + "-" + guid.Substring(0, 4);

            return id;
        }

        public static JB2Image GenerateBingoCardImage(IBingoCard cardData, string formatCode, JB2Image backgroundImage)
        {

            int[] locationX = null;
            int[] locationY = null;
            string fontCode = string.Empty;
            int fontSize = 18;

            switch (formatCode.ToLower())
            {
                case "bingoforever":
                    locationX = new int[] { 25, 100, 168, 243, 315 };
                    locationY = new int[] { 90, 160, 228, 300, 369 };
                    fontCode = "ffft1";
                    fontSize = 40;
                    break;
            }

            using (Graphics graphics = Graphics.FromImage(backgroundImage))
            {
                using (Font arialFont = JB2.Helpers.FontHelper.GetFont(fontCode, fontSize))
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

                using (Font copyrightFont = new Font("Consolas",14))
                {
                    PointF idLocation = new PointF(25, 450);

                    string words = "Card ID: " +cardData.ID;

                    graphics.DrawString(words, copyrightFont, Brushes.White, idLocation);

                }
            }
            System.IO.MemoryStream imageStream = new System.IO.MemoryStream();

            ((System.Drawing.Image)backgroundImage).Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);

            byte[] imageBytes = backgroundImage.FileContent;

            return backgroundImage;

        }

        public static JB2.Common.ServiceResult IsBingoWinner(byte[,] cardValues, byte[] calledBingoBalls, BingoPatternType  bingopattern )
        {

            bool isWinner = false;

            int maxRows = cardValues.GetLength(0);
            int maxColumns = cardValues.GetLength(1);

            List<bool> marks = new List<bool>(maxRows * maxColumns);



            for (int r = 0; r < maxRows; r++)
            {
                for (int c = 0; c < maxColumns; c++)
                {
                    marks.Add( calledBingoBalls.Contains( cardValues[r,c]) ? true : false);
                }
            }

            return IsBingoWinner(new System.Collections.BitArray(marks.ToArray()), bingopattern.ToBitArray());
        }

        public static JB2.Common.ServiceResult IsBingoWinner(System.Collections.BitArray  marks, System.Collections.BitArray[] winningPatterns)
        {
            JB2.Common.ServiceResult isWinner = false;
            foreach(BitArray pattern in winningPatterns)
            {
                if( marks.And(pattern) == pattern)
                {
                    isWinner.Validation.Add(new Validation("WinningPattern", pattern.ToString()));
                }
            }
            return isWinner;

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
