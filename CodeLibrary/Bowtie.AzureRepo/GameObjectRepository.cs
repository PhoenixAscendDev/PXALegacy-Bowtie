using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

using JB2.Bowtie.GameObjects;
using JB2.Common;
using JB2.Common.Data;

using JB2.Bowtie.Enum;

namespace JB2.Bowtie.Data.Azure
{
    public class GameObjectRepository : JB2.Bowtie.IGameObjectRepository
    {
        private JB2.Common.Data.AzureTableRepository _table;
        private JB2.Common.Data.AzureBlobRepository _blob;

        private readonly string BINGOCARDIMAGE_FILENAME = "bingocard/{0}/{1}.png";


        #region Constructors

        //public GameObjectRepository()
        //{
        //    _table = AzureStorage.GameObjectsTable;
        //    _blob = AzureStorage.GameObjectsBlob;
        //}

        public GameObjectRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob)
        {
            _table = azureTable;
            _blob = azureBlob;
        }

        #endregion Constructors


        #region Bingo 
        public ServiceResult InsertBingoCard(IBingoCard card)
        {
            string partitionKeyFormat = "bingocard:{0}";
            BingoCardEntry cardEntry = new BingoCardEntry();

            cardEntry.RowKey = card.ID;
            switch (card.BingoType)
            {
                case BingoType.Standard:
                    cardEntry.PartitionKey = string.Format(partitionKeyFormat, "STA");
                    break;
            }
            switch (card.CardSize)
            {
                case BingoCardSize.s5:
                    cardEntry.Size = card.CardSize.ToString();
                    cardEntry.Rows = 5;
                    cardEntry.Columns = 5;
                    break;
            }

            cardEntry.RowKey = card.ID;

            cardEntry.DateCreated = DateTime.Today.ToString();
            cardEntry.CardID = card.ID;
            cardEntry.BingoType = card.BingoType.ToString();
            cardEntry.Spaces = JB2.Common.Utility.ObjectToString(card.Cells);
            cardEntry.CheckSum = BingoHelper.CalculateChecksum(card.Cells);
            cardEntry.UniqueToken = card.UniqueToken;


            _table.Insert<BingoCardEntry>(cardEntry);

            return true;

        }

        public IBingoCard GetBingoCardByID(string id)
        {
            IBingoCard result;
            string typeCode = id.Split('-')[0];
            BingoCardEntry cardEntry = _table.GetEntity<BingoCardEntry>("bingocard:" + typeCode, id);
            string guid = Regex.Split(cardEntry.UniqueToken, ">*<")[0]; //cardEntry.UniqueToken.Split(">*<")[0];
            switch (typeCode)
            {
                default:
                    result = new StandardBingoCard(Enum.BingoType.Standard, true,guid);
                    break;
            }
            result.Cells = JB2.Common.Utility.ObjectFromString(cardEntry.Spaces) as byte[,];
            return result;
        }

        public ServiceResult InsertBingoCardImage(string id, string styleCode, JB2Image image)
        {
            return _blob.Insert(image.FileContent, string.Format(BINGOCARDIMAGE_FILENAME, styleCode, id));
        }

        public ServiceResult InsertBingoCardImage(IBingoCard card, string styleCode)
        {
            // Get style base
            string filename = "bingoCardStyle/" + styleCode + ".png";
            byte[] imageBytes = _blob.GetByteArray(filename);

            JB2Image imageToSave = BingoHelper.GenerateBingoCardImage(card, JB2Image.FromByteArray(imageBytes));

            return _blob.Insert(imageToSave.FileContent, string.Format(BINGOCARDIMAGE_FILENAME, styleCode, card.ID));
        }

        public JB2Image GetBingoCardImage(IBingoCard card, string styleCode)
        {
            return GetBingoCardImage(card.ID, styleCode);
        }
        public JB2Image GetBingoCardImage(string id, string styleCode)
        {
            return JB2Image.FromByteArray(_blob.GetByteArray(string.Format(BINGOCARDIMAGE_FILENAME, styleCode, id)));
        }

        public JB2Image GetBingoCardStyle(string styleCode)
        {
            string filename = "bingoCardStyle/" + styleCode + ".png";
            byte[] imageBytes = _blob.GetByteArray(filename);

            return JB2Image.FromByteArray(imageBytes);
        }

        private bool DoesBingoCardImageExist(string id, string styleCode)
        {
            byte[] result = _blob.GetByteArray(string.Format(BINGOCARDIMAGE_FILENAME, styleCode, id));
            return result != null;
        }

        #endregion Bingo

        #region Color

        public ServiceResult InsertColor(IColor c)
        {
            ColorEntry cEntry = new ColorEntry("color", c.ID);

            cEntry.HexString = c.Color.HexString;
            cEntry.HexInt = c.Color.HexValue;

            cEntry.HSV_Hue = c.Color.HSV.Hue;
            cEntry.HSV_Saturation = c.Color.HSV.Saturation;
            cEntry.HSV_Value = c.Color.HSV.Value;

            cEntry.RGB_Red = c.Color.RGB.Red;
            cEntry.RGB_Green = c.Color.RGB.Green;
            cEntry.RGB_Blue = c.Color.RGB.Blue;

            //cEntry.UniqueToken = c.UniqueToken;
            cEntry.Name = c.Name;
            cEntry.ID = c.ID;
            cEntry.ColorSetName = c.ColorSet.ToString();
            cEntry.ColorType = "Solid";

            //insert main partition
            _table.Insert<ColorEntry>(cEntry,true);

            //insert colorHex partition
            cEntry.PartitionKey = "colorHex:" + c.Color.HexString[0];
            cEntry.RowKey = "Hex:" + c.HexValue;
            _table.Insert<ColorEntry>(cEntry,true);



            //insert colorset partition
            //don't bother if Set is None
            if (c.ColorSet != ColorSetType.None)
            {
                cEntry.PartitionKey = "colorSet:" + c.ColorSet.ToString();
                cEntry.RowKey = "Hex:" + c.HexValue;
                _table.Insert<ColorEntry>(cEntry, true);

                cEntry.PartitionKey = "colorSet:" + c.ColorSet.ToString();
                cEntry.RowKey = "ID:" + c.ID;
                _table.Insert<ColorEntry>(cEntry, true);

                cEntry.PartitionKey = "colorSet:" + c.ColorSet.ToString();
                cEntry.RowKey = "Name:" + c.Name;
                _table.Insert<ColorEntry>(cEntry, true);
            }

            //insert basic Bowtie object partition
            cEntry.PartitionKey = "bowtieObject";
            //cEntry.RowKey = c.UniqueToken;
            //_table.Insert<ColorEntry>(cEntry);

            return true;


        }

        public IColor GetColorByID(string id)
        {
            try
            {
                ColorEntry entry = _table.GetEntity<ColorEntry>("color", id);
                return getColor(entry);

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public IColor GetColorByName(string name, JB2.Bowtie.Enum.ColorSetType set)
        {
            try
            {
                ColorEntry entry = _table.GetEntity<ColorEntry>("colorSet:" + set.ToString(), "Name:" + name);
                return getColor(entry);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IColor GetColorByHex(string hexString)
        {
            try
            {
                ColorEntry entry = _table.GetEntity<ColorEntry>("colorHex:" + hexString[0], "Hex:" + hexString);
                if (entry != null)
                    return getColor(entry);
                else
                    return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IColor[] GetColorsByColorSet(JB2.Bowtie.Enum.ColorSetType type)
        {
            var entries = _table.GetByRowKeyStartWith<ColorEntry>("colorSet:" + type.ToString(), "Hex:", 300);

            List<IColor> colors = new List<IColor>(entries.Count());

            foreach(ColorEntry e in entries)
            {
                colors.Add(getColor(e));
            }

            return colors.ToArray();

        }

        private IColor getColor(ColorEntry entry)
        {
            IColor result = null;
            switch (entry.ColorType.ToUpper())
            {
                case "SOLID":
                    result = new SolidColor(entry.ID, entry.HexString, (ColorSetType)System.Enum.Parse(typeof(ColorSetType), entry.ColorSetName));
                    break;
            }

            result.Name = entry.Name;
            result.ID = entry.ID;

            return result;

        }



        public ServiceResult InsertColorSet(IColorSet s)
        {
            ColorSetEntry entry = new ColorSetEntry("ColorSet", "ColorSetType:" + s.Type.ToString());
            entry.ID = s.ID;
            entry.Name = s.Name;
            entry.ColorCount = s.Count;
            entry.ColorSetType = s.Type.ToString();

            //insert into Azure Table Repo
            _table.Insert<ColorSetEntry>(entry, true);

            return true;
        }

        public IColorSet GetColorSet(ColorSetType type)
        {
            try
            {
                ColorSetEntry entry = _table.GetEntity<ColorSetEntry>("ColorSet", "ColorSetType:" + type.ToString());

                if (entry != null)
                {

                    //get the colors in the set
                    var colors = this.GetColorsByColorSet(type);
                    ColorSet set = new ColorSet(entry.ID, (ColorSetType)System.Enum.Parse(typeof(ColorSetType), entry.ColorSetType),colors);
                    return set;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                return null;
            }


        }


        #endregion Color

        public void Insert(IGameObject entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IGameObject entity)
        {
            throw new NotImplementedException();
        }

        public IGameObject[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IGameObject[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IGameObject GetById(string id)
        {
            throw new NotImplementedException();
        }

        
    }
}
 
  


