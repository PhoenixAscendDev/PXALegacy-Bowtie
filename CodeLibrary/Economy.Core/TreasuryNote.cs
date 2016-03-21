using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public abstract class TreasuryNote : ITreasuryNote
    {
        #region Fields
        protected long _amount;
        protected string _id;
        protected JB2.Common.IIDNamePair<string,string> _requestor;

        #endregion Fields

        #region Properties

        public long Amount
        {
            get
            {
                return _amount;
            }
        }

        public string ID
        {
            get
            {
                return _id;
            }
        }

        public abstract JB2.Common.IIDNamePair<string,string> GetRequestor();

        #endregion Properties
    }
}
