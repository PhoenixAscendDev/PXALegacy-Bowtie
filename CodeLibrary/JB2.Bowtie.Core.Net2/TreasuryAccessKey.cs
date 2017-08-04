using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public struct TreasuryRequestKey
    {
        #region Fields

        string _treasuryID;
        string _requestKey;

        #endregion Fields

        #region Constructors

        public TreasuryRequestKey(string treasuryID, string requestKey)
        {
            _treasuryID = treasuryID;
            _requestKey = requestKey;
        }

        #endregion Constructors

        public string TreasuryID
        {
            get
            {
                return _treasuryID;
            }
            set
            {
                _treasuryID = value;
            }

        }
        public string Key
        {
            get
            {
                return _requestKey;
            }
            set
            {
                _requestKey = value;
            }

        }

        #region Methods

        #region To Methods
        public KeyValuePair<string, string> ToKeyValuePair()
        {
            return (KeyValuePair<string, string>)this;
        }

        public JB2.Common.IDNamePair<string, string> ToIDNamePair()
        {
            return (JB2.Common.IDNamePair<string, string>)this;
        }

        public override string ToString()
        {
            return _treasuryID + ":" + _requestKey;
        }

        #endregion To Methods

        #endregion Methods


        #region Static
        public static readonly TreasuryRequestKey Empty = new TreasuryRequestKey(string.Empty, string.Empty);
        #endregion Static

        #region implicit operators
        public static implicit operator KeyValuePair<string,string>(TreasuryRequestKey trk)
        {
            var kvp = new KeyValuePair<string, string>(trk.TreasuryID, trk.Key);

            return kvp;
        }

        public static implicit operator JB2.Common.IDNamePair<string,string>(TreasuryRequestKey trk)
        {
            var inv = new JB2.Common.IDNamePair<string, string>(trk.TreasuryID, trk.Key);
            return inv;
        }

        #endregion implicit operators

        

    }
}
