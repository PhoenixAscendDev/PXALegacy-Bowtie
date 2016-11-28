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

        private static StorageAccount ConfigStorage
        {
            get
            {
                var name = JB2.Configuration.GetAppSetting("JB2:bowtie-configStorageName");
                var key = JB2.Configuration.GetAppSetting("JB2:bowtie-configStorageKey"); ;
                return StorageAccount.FromAzureStorage(name, key);
            }
        }

        private static StorageAccount ApplicationDataStorage
        {
            get
            {
                var name = JB2.Configuration.GetAppSetting("JB2:bowtie-appStorageName");
                var key = JB2.Configuration.GetAppSetting("JB2:bowtie-appStorageKey"); ;
                return StorageAccount.FromAzureStorage(name, key);
            }
        }

        private static StorageAccount PlayerDataStorage
        {
            get
            {
                var name = JB2.Configuration.GetAppSetting("JB2:bowtie-playerdataStorageName");
                var key = JB2.Configuration.GetAppSetting("JB2:bowtie-playerdataStorageKey"); ;
                return StorageAccount.FromAzureStorage(name, key);
            }
        }


        public static AzureBlobRepository GameObjectsBlob
        {
            get
            {
                return JB2.Infrastructure.Storage.BowtieAccount.GetBlog("gameobjects");
                //return new AzureBlobRepository(_BowtieAccount, "gameobjects");
            }
        }

        public static AzureBlobRepository GeneralBlob
        {
            get
            {
                return JB2.Infrastructure.Storage.BowtieAccount.GetBlog("general");
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

        public static AzureTableRepository GraphTable
        {
            get
            {
                return JB2.Infrastructure.Storage.GraphAccount.GetTable("bowtie");
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

        public static AzureTableRepository ApplicationTable
        {
            get
            {
                return ConfigStorage.GetTable("applications");
            }
        }

        public static AzureTableRepository AchievementTable
        {
            get
            {
                return ConfigStorage.GetTable("achievements");
            }
        }

        public static AzureTableRepository PointSystemTable
        {
            get
            {
                return ConfigStorage.GetTable("pointsystems");
            }
        }

        public static AzureTableRepository PlayerAchievementTable
        {
            get
            {
                return PlayerDataStorage.GetTable("achievements");
            }
        }

        public static AzureTableRepository DewdropTable
        {
            get
            {
                return ConfigStorage.GetTable("dewdrops");
            }
        }

        public static AzureTableRepository PlayerDewdropTable
        {
            get
            {
                return PlayerDataStorage.GetTable("dewdrops");
            }
        }



        public static AzureTableRepository AuthorizeTable
        {
            get
            {
                return JB2.Infrastructure.Storage.APIKeyAccount.GetTable("bowtie");
            }
        }

        public static AzureTableRepository LogTable
        {
            get
            {
                return JB2.Infrastructure.Storage.LogAccount.GetTable("bowtie");
            }
        }


        public static AzureQueueRepository DewdropQueue
        {
            get
            {
               return JB2.Infrastructure.Storage.BowtieAccount.GetQueue("dewdrop");
            }
        }

        public static AzureQueueRepository MaintenanceQueue
        {
            get
            {
                return JB2.Infrastructure.Storage.BowtieAccount.GetQueue("maintenanceTasks");
            }
        }

        public static AzureQueueRepository AchievementQueue
        {
            get
            {
                return JB2.Infrastructure.Storage.BowtieAccount.GetQueue("achievement");
            }
        }




    }
}
