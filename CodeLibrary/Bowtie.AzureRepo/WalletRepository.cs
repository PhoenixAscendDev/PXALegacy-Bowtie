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
        public WalletRepository() : this("appdata","general")
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
            throw new NotImplementedException();
        }

        protected override IWallet convertToObject(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            throw new NotImplementedException();
        }
    }
}
