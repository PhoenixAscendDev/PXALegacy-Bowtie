using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class InventoryItem : JB2.Common.JB2Class, IInventoryItem
    {    
        public InventoryItem(string id) : base()
        {
            this.SetProperty<string>("ID", id);
        }

        //public string PlayerID
        //{
        //    get
        //    {
        //        return this.GetProperity<string>("PlayerID", string.Empty);
        //    }
        //    set
        //    {
        //        this.SetProperty<string>("PlayerID", value);
        //    }
        //}

        public Common.JB2Image Icon
        {
            get
            {
                return this.GetProperity<Common.JB2Image>("Icon", new Common.JB2Image());
            }

            set
            {
                this.SetProperty<Common.JB2Image>("Icon", value);
            }
        }

        public string ID
        {
            get
            {
                return this.GetProperity<string>("ID", string.Empty);
            }

            set
            {
                this.SetProperty<string>("ID", value);
            }
        }

        public string Name
        {
            get
            {
                return this.GetProperity<string>("Name", string.Empty);
            }

            set
            {
                this.SetProperty<string>("Name", value);
            }
        }

        public string PuralName
        {
            get
            {
                return this.GetProperity<string>("PuralName", this.Name);
            }
            set
            {
                this.SetProperty<string>("PuralName", value);
            }
        }

        //public int Quanity
        //{
        //    get
        //    {
        //        return this.GetProperity<int>("Quantity", 0);
        //    }

        //    set
        //    {
        //        this.SetProperty<int>("Quantity", value);
        //    }
        //}

        public string InventoryCategory
        {
            get
            {
                return this.GetProperity<string>("Type", string.Empty);
            }

            set
            {
                this.SetProperty<string>("Type", value);
            }
        }

        public string GetID()
        {
            return this.ID;
        }

        public string GetName()
        {
            return this.Name;
        }

        //public string GetPlayerID()
        //{
        //    return this.PlayerID;
        //}


        public static InventoryItem New
        {
            get
            {
                string id = JB2.Common.NewID.ShortGuid();

                return new InventoryItem(id);
            }
        }
    }
}
