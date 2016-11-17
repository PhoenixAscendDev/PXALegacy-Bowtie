using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class GraphElementAttribute : Attribute
    {
        #region Fields
        protected string _id;
        #endregion Fields
        #region Constructor
        public GraphElementAttribute(string id)
        {
            _id = id;
        }
        #endregion Constructor

        #region Properties
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        #endregion Properties

    }

}
