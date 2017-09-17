using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie
{
    public class BasicDewdrop : IDewdrop
    {

        #region Constructors

        public BasicDewdrop()
        {


            GDID = JB2.Helper.Bowtie.GenerateID<IDewdrop>();

            ID = 0;// JB2.Common.NewID.UriHash(new Uri("http://bowtie.io/?=" + GDID + ApplicationID));


        }



        #endregion Constructors


        public string ApplicationID { get; set; }
        public string GDID { get; set; }
        public string ParentGDID { get; set; }
        public bool IsActive { get; set; }
        public byte ID { get; set; }
        public string Name { get; set; }
        public DewDropValueType ValueType { get; set; }

        public byte GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }

        
    }
}
