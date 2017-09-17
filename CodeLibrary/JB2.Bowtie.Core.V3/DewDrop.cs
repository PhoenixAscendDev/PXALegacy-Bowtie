using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public class Dewdrop : IDewdrop
    {

        #region Constructors

        public Dewdrop()
        {


            GDID = JB2.Helper.Bowtie.GenerateID<IDewdrop>();

            ID = JB2.Common.NewID.UriHash(new Uri("http://bowtie.io/?=" + GDID + ApplicationID));


        }



        #endregion Constructors


        public string ApplicationID { get; set; }
        public string GDID { get; set; }
        public string ParentGDID { get; set; }
        public bool IsActive { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public DewDropValueType ValueType { get; set; }

        public string GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
