using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie.Economy
{
    public class TokenCurrency : JB2.Common.IDNamePair, ICurrency
    {

        #region Fields
        protected IDenomination[] _denominations;
        protected string _puralname;
        protected string _symbolUri;

        #endregion Fields

        public TokenCurrency() : base (string.Empty,string.Empty)
        {

        }
        public virtual int BaseUnit
        {
            get
            {
                return 1;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public CurrencyType CurrencyType
        {
            get
            {
                return CurrencyType.NoSubUnit;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IDenomination[] Denominations
        {
            get
            {
                return _denominations;
            }

            set
            {
                _denominations = value;
            }
        }

        public string PluralName
        {
            get
            {
                if (string.IsNullOrEmpty(_puralname))
                    return this.Name + "s";
                else
                    return _puralname;
            }

            set
            {
                _puralname = value;
            }
        }

        public float[] SubUnits
        {
            get
            {
                return new float[0];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SymbolUrl
        {
            get
            {
                return _symbolUri;
            }

            set
            {
                _symbolUri = value;
            }
        }

         
    }
}
