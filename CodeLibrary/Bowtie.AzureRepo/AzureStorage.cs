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

        //private static CloudStorageAccount _BowtieAccount
        //{
        //    get
        //    {
        //        return JB2.Common.Data.AzureHelper.GetStorageAccount("jb2bowtie", "frIlemrNzlvAbKNhiyYCeW+otbFXBoJb0TodzbgwzF8IBEZMtifrHfx0Y+o1+jwvUL4FcAGYepHlgqrG0iCc1Q==");
        //    }
        //}

        //private static CloudStorageAccount _JB2Account
        //{
        //    get
        //    {
        //        return JB2.Common.Data.AzureHelper.GetStorageAccount("jbsquared", "iGf7AhI5v12hig85TsVkJSuPwvB42EncTMFogXFcqGlEcVMo5oXf0PvsMkxOQQeDmM21UtqHHtwyjR3MG3Di5g==");
        //    }
        //}

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




    }
}
