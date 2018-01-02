using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie
{
    public class BasicDewdrop : JB2.Sprog.SprogItem, IDewdrop
    {

        #region Constructors

        public BasicDewdrop() : base(JB2.Helper.Bowtie.DewDropSprog)
        {


            GDID = JB2.Helper.Bowtie.GenerateID<IDewdrop>();

            ID = 0;// JB2.Common.NewID.UriHash(new Uri("http://bowtie.io/?=" + GDID + ApplicationID));


        }



        #endregion Constructors


        public string ApplicationID
        {
            get
            {
                return this.GetProperty<string>("APPLICATIONID", string.Empty);
            }
            set
            {
                this.SetProperty<string>("APPLICATIONID", value);
            }
        }
        public string GDID
        {
            get
            {
                return this.GetProperty<string>("GDID", string.Empty);
            }
            set
            {
                this.SetProperty<string>("GDID", value);
            }
        }
        public string ParentGDID
        {
            get
            {
                return this.GetProperty<string>("PARENTGDID", string.Empty);
            }
            set
            {
                this.SetProperty<string>("PARENTGDID", value);
            }
        }
        public bool IsActive
        {
            get
            {
                return this.GetProperty<bool>("ISACTIVE", false);
            }
            set
            {
                this.SetProperty<bool>("ISACTIVE", value);
            }
        }
        public  byte ID
        {
            get
            {
                return this.GetProperty<byte>("ID", 0);
            }
            set
            {
                this.SetProperty<byte>("ID", value);
            }
        }
        public  string Name
        {
            get
            {
                return this.GetProperty<string>("NAME", string.Empty);
            }
            set
            {
                this.SetProperty<string>("NAME", value);
            }
        }
        public DewDropValueType ValueType
        {
            get
            {
                return (DewDropValueType)this.GetProperty<int>("VALUETYPE", (int)DewDropValueType.Count);
            }
            set
            {
                this.SetProperty<int>("VALUETYPE", (int)value);
            }
        }

        public  byte GetID()
        {
            return ID;
        }

        public  string GetName()
        {
            return Name;
        }

        public string GetApplicationID()
        {
            return ApplicationID;
        }


    }
}
