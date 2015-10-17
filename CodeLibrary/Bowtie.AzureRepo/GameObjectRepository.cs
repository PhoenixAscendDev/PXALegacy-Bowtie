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

        public GameObjectRepository()
        {
            _table = AzureStorage.GameObjectsTable;
            _blob = AzureStorage.GameObjectsBlob;
        }

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

        #endregion

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
 
  


