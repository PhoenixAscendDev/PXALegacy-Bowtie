using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Azure
{
    public class PlayerEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        public PlayerEntry() : base()
        {

        }
        public PlayerEntry(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {

        }

        public string Title { get;set;}
        public string jBeanAccountNumber { get; set; }
        public string BitScore { get; set; }
        public string KenshinID { get; set; }
        public string KenshinName { get; set; }
        public string ID { get; set; }








    }
}
