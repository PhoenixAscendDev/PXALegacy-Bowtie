using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Azure
{
    public class BingoCardEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        public BingoCardEntry(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }


        string CheckSum { get; set; }
        string CardID { get; set; }

        string Spaces { get; set; }

        string BingoType { get; set; }

        int Rows { get; set; }
        int Columns { get; set; }

        string Size { get; set; }

        string DateCreated { get; set; }
    }
}
