using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string partitionKeyFormat = "bingcard:{0}";
            BingoCardEntry cardEntry = new BingoCardEntry();

            cardEntry.RowKey = card.ID;
            switch(card.BingoType)
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
            cardEntry.DateCreated = DateTime.Today.ToString();
            cardEntry.CardID = card.ID;
            cardEntry.BingoType = card.BingoType.ToString();
            cardEntry.Spaces = JB2.Common.Utility.ObjectToString(card.Cells);
            cardEntry.CheckSum = BingoHelper.CalculateChecksum(card.Cells);

            _table.Insert<BingoCardEntry>(cardEntry);

            return true;

        }

        public IBingoCard GetBingoCardByID(string id)
        {

            IBingoCard result;
            string typeCode = id.Split('-')[0];
            BingoCardEntry cardEntry = _table.GetEntity<BingoCardEntry>("bingocard:" + typeCode, id);

            switch(typeCode)
            {

                default :
                    result = new StandardBingoCard(Enum.BingoType.Standard, true, id);
                    break;
            }
            result.Cells = JB2.Common.Utility.ObjectFromString(cardEntry.Spaces) as byte[,];
            return result;          
        }


        #endregion 



        public void Delete(IGameObject entity)
        {
            throw new NotImplementedException();
        }

        public IGameObject[] GetAll()
        {
            throw new NotImplementedException();
        }



        public IGameObject GetById(string id)
        {
            IGameObject result = null;
            string[] stringSeparators = new string[] { ">*<" };
            GameObjectType goType = (GameObjectType)System.Enum.Parse(typeof(GameObjectType), id.Split(new string[] { ">*<" }, StringSplitOptions.None)[0]);

            switch(goType)
            {
                case GameObjectType.BingoCard:
                    result = GetBingoCardByID(id.Split(new string[] { ">*<" }, StringSplitOptions.None)[1]);
                    break;
            }

            return result;



        }

        public void Insert(IGameObject entity)
        {
            switch(entity.GameObjectType)
            {
                case GameObjectType.BingoCard:
                    this.InsertBingoCard((IBingoCard)entity);
                    break;
            }
        }


        public IGameObject[] SearchFor()
        {
            throw new NotImplementedException();
        }



    }
}
