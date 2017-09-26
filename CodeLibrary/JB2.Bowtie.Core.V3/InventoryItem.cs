using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class BasicInventoryItem : BowtieObject, IInventoryItem
    {
        public string ApplicationID { get; set; }
        public string IconUrl { get; set; }
        public string InventoryCategory { get; set; }
        public string PuralName { get; set; }
        public string StorageSlot { get; set; }

        public string GetApplicationID()
        {
            return ApplicationID;
        }
    }
}
