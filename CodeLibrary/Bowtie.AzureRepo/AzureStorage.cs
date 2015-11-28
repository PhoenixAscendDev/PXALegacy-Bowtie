using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using JB2.Common.Data;

namespace JB2.Bowtie.Data.Azure
{
    public static class AzureStorage
    {


        public static AzureBlobRepository GameObjectsBlob
        {
            get
            {
                return JB2.Infrastructure.Storage.BowtieAccount.GetBlog("gameobjects");
                //return new AzureBlobRepository(_BowtieAccount, "gameobjects");
            }
        }

        public static AzureTableRepository GameObjectsTable
        {
            get
            {
                return JB2.Infrastructure.Storage.BowtieAccount.GetTable("gameobjects");
                //return new AzureTableRepository(_BowtieAccount, "gameobjects");
            }
        }

        public static AzureTableRepository PlayersTable
        {
            get
            {
                return JB2.Infrastructure.Storage.BowtieAccount.GetTable("players");
            }
        }




    }
}
