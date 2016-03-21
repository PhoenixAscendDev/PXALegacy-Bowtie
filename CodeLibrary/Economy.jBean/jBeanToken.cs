using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class jBeanToken : JB2.Common.IDNamePair<string,string>
    {
        #region Fields
        protected Enum.JBeanTokenType _tokenType;
        protected long _value { get; set; }
        protected string _treasuryNote { get; set; }
        protected string _owner { get; set; }
        #endregion Fields


        #region Properties
        public Enum.JBeanTokenType TokenType
        {
            get { return _tokenType; }
            set { _tokenType = value; }
        }
        public long Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string TreasuryNoteId
        {
            get { return _treasuryNote; }
            set { _treasuryNote = value; }
        }

        

        #endregion Properties



        public JBeanDenomination GetDenomination()
        {
            return new JBeanDenomination(this._tokenType);
        }
        #region Implicit Operators

        public static implicit operator long(jBeanToken e)
        {
            return e.Value;
        }
        #endregion Implicit Operators
    }
}
