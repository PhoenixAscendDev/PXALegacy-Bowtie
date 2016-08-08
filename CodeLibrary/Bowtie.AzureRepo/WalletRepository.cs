using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Table;
using JB2.Common;
using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public class WalletRepository : BowtieRepository<IWallet, DynamicTableEntity>, IWalletRepository
    {

        #region Constructors
        public WalletRepository() : this("wallets","general")
        {

        }

        protected WalletRepository(string tableName, string blobName)
            : this(JB2.Infrastructure.Storage.BowtieAccount.GetTable(tableName),
                   JB2.Infrastructure.Storage.BowtieAccount.GetBlog(blobName))
        {

        }

        public WalletRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob) : base()
        {
            _table = azureTable;
            _blob = azureBlob;
            _defaultPartitionKey = "wallet";
        }

        #endregion

        public IWallet GetByPlayerAndApplication(string playerID, string appID)
        {
            var e = _table.GetByRowKeyStartWith<DynamicTableEntity>("wallet:application:" + appID, "playerid:" + playerID, 1000).FirstOrDefault();           
            return convertToObject(e);
        }


        protected override DynamicTableEntity convertToEntity(IWallet o)
        {
            var e = new DynamicTableEntity();
            var bag = o.JBeanTotal;

            e.Properties.Add("ID", new EntityProperty(o.GetID()));
            e.Properties.Add("PlayerID", new EntityProperty(o.Owner.GetID()));
            e.Properties.Add("ApplicationID", new EntityProperty(o.GetApplicationID()));
            e.Properties.Add("JBean_KidneyTotal", new EntityProperty(bag.Kidney));
            e.Properties.Add("JBean_NavyTotal", new EntityProperty(bag.Navy));
            e.Properties.Add("JBean_PintoTotal", new EntityProperty(bag.Pinto));
            return e;
        }

        protected override IEnumerable<IWallet> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            var results = new List<IWallet>();

            foreach(var e in list)
            {
                results.Add(convertToObject(e));
            }
            return results;
        }

        protected override IWallet convertToObject(DynamicTableEntity e)
        {
            if (e != null)
            {
                var jBeans = new JB2.Economy.JBeanCollection();

                var playerID = e.Properties.ContainsKey("PlayerID") ? e.Properties["PlayerID"].StringValue : string.Empty;
                var appID = e.Properties.ContainsKey("ApplicationID") ? e.Properties["ApplicationID"].StringValue : string.Empty;
                var id = e.Properties.ContainsKey("ID") ? e.Properties["ID"].StringValue : string.Empty;


                var treasuryNotes = _table.GetByRowKeyStartWith<DynamicTableEntity>("wallet:" + id, "treasurynote:", 1000);


                var app = JB2.Settings.Bowtie.UnitOfWork.ApplicationRepository.GetById(appID);
                foreach(var n in treasuryNotes)
                {
                    var noteID = n.Properties.ContainsKey("ID") ? n.Properties["ID"].StringValue : string.Empty;
                    var amount = n.Properties.ContainsKey("Amount") ? n.Properties["Amount"].Int64Value : 0;

                    var tempNote = new JB2.Economy.JbeanTreasuryNote(noteID, (int)amount, app);

                    var isValid = JB2.Settings.Jbean.Factory.Treasury.IsValidNote(tempNote);

                    if(isValid)
                        jBeans.Add(tempNote);
                }



                IWallet wallet = new PlayerWallet(id, playerID, jBeans);
                wallet.ApplicationID = appID;

                return wallet;
            }
            return null;
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            e.PartitionKey = "wallet";
            e.RowKey = "id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e,true);

            e.PartitionKey = "wallet:application:" + e.Properties["ApplicationID"].StringValue;
            e.RowKey = "playerid:" + e.Properties["PlayerID"].StringValue + "_id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, true);

            e.PartitionKey = "wallet:player:" + e.Properties["ApplicationID"].StringValue;
            e.RowKey = "appid:" + e.Properties["ApplicationID"].StringValue + "_id:" + e.Properties["ID"].StringValue;
            _table.Insert<DynamicTableEntity>(e, true);
        }


        private void saveTreasuryNote(IWallet wallet, JB2.Economy.ITreasuryNote note)
        {
            var e = new DynamicTableEntity();

            e.Properties.Add("ID", new EntityProperty(note.ID));
            e.Properties.Add("Amount", new EntityProperty(note.Amount));
            e.Properties.Add("IssuedBy", new EntityProperty(note.GetRequestor().GetID()));

            e.PartitionKey = "wallet:" + wallet.GetID();
            e.RowKey = "treasurynote:" + note.ID;
            _table.Insert(e, true);

        }

        private void saveReceipt(IWallet wallet, WalletReceipt receipt)
        {
            var e = new DynamicTableEntity();
            e.Properties.Add("CurrencyID", new EntityProperty(receipt.CurrencyID));
            e.Properties.Add("Description", new EntityProperty(receipt.Description));
            e.Properties.Add("Amount", new EntityProperty(receipt.Amount));
            e.Properties.Add("TransationDate", new EntityProperty(receipt.TransactionDate));
            e.Properties.Add("TransationType", new EntityProperty(receipt.TransactionType.ToString()));
            e.Properties.Add("ID", new EntityProperty(receipt.TransationID));
            e.PartitionKey = "wallet:" + wallet.GetID();
            e.RowKey = "receipt:" + receipt.TransationID;
            _table.Insert(e, true);
        }
        public override void Insert(IWallet obj)
        {
            var e = convertToEntity(obj);

            foreach(var n in obj.GetTreasuryNotes())
            {
                saveTreasuryNote(obj, n);
            }

            foreach (var r in obj.GetReceipts())
            {
                saveReceipt(obj, r);
            }

            saveEntity(e,true);
        }

    }
}
