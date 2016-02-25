using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Reflection;

using JB2.Bowtie;
using JB2.Common;

namespace JB2.Bowtie.WebAPI.Models
{
    public class DewdropViewModel:  JB2.Common.WebAPI.APIObject
    {
        public DewdropViewModel(Dewdrop drop)
        {
            this.ApplicationID = drop.GetApplicationID();
            this.Description = drop.GetDescription();
            this.GraphID = drop.GetGraphID();
            this.ID = drop.GetID();
            this.Name = drop.GetName();

            base.serializableProperties = new List<string>();

            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                this.serializableProperties.Add(p.Name);
            }

        }

        public string ApplicationID { get; set; }
        public string Description { get; set; }
        public string GraphID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }

    }
}