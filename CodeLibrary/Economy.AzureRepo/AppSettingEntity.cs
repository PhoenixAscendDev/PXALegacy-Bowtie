using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Data;

namespace JB2.Economy.Data
{
    public class AppSettingEntity : AzureTableEntity
    {
        public AppSettingEntity(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {

        }
        
        public AppSettingEntity() : base()
        {

        }

        public bool CanRequest { get; set; }
        public string RequestValidationKey { get; set; }
        public string ApplicationID { get; set; }
    }
}
