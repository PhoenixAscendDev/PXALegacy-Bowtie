using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Azure
{
    public class ColorEntry : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {


        public ColorEntry(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public ColorEntry()
        {

        }

        public string ID { get; set; }

        public string Name { get; set; }

        public string HexString { get; set; }

        public int HexInt { get; set; }

        public int RGB_Red { get; set; }
        public int RGB_Green { get; set; }

        public int RGB_Blue { get; set; }

        public double HSV_Hue { get; set; }

        public double HSV_Saturation { get; set; }

        public double HSV_Value { get; set; }


        public string UniqueToken { get; set; }

        public string ColorSetName { get; set; }

        public string ColorType { get; set; }

    }
}
