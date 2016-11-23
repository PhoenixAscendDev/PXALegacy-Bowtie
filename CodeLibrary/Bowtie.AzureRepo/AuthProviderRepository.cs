using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public class AuthProviderRepository : BowtieRepository<IAuthProvider>, IAuthProviderRepository
    {

        #region Constructors


        public AuthProviderRepository() :  this("authproviders", "general")
        {

        }
        public AuthProviderRepository(string tableName, string blobName)
            : this(JB2.Infrastructure.Storage.BowtieAccount.GetTable(tableName),
                   JB2.Infrastructure.Storage.BowtieAccount.GetBlog(blobName))
        {
            
        }

        public AuthProviderRepository(AzureTableRepository azureTable, AzureBlobRepository azureBlob)
        {
            _table = azureTable;
            _blob = azureBlob;
            _defaultPartitionKey = "authprovider";
        }

        #endregion Constructors



        protected override DynamicTableEntity convertToEntity(IAuthProvider o)
        {
            DynamicTableEntity e = new DynamicTableEntity();
            e.SetProperty<string>("ID", o.ID);
            e.SetProperty<string>("Name", o.Name);

            return e;
        }

        protected override IEnumerable<IAuthProvider> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            List<IAuthProvider> alist = new List<IAuthProvider>();

            foreach(var e in list)
            {
                alist.Add(convertToObject(e));
            }

            return alist;
        }

        protected override IAuthProvider convertToObject(DynamicTableEntity e)
        {
            AuthProvider a = new AuthProvider();

            a.Name = e.GetPropertyValue<string>("Name",string.Empty);
            a.ID = e.GetPropertyValue<string>("ID", string.Empty);

            return a;

        }

        protected override void deleteEntry(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            
            e.PartitionKey = _defaultPartitionKey ;
            e.RowKey = "id:" + e.GetPropertyValue<string>("ID", string.Empty);
            _table.Insert<DynamicTableEntity>(e, replace);
        }
    }
}
