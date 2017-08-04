using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public struct PointSystemConfig : JB2.Common.IIDNamePair<string, string>
    {
        public string Namespace { get; set; }

        public string ClassName { get; set; }


        #region IIDNamePair
        public string ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }

        #endregion IIDNamePair
    }
}