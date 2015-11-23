using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public class BasicCurrency : ICurrency<string,byte,Token,JB2.Common.IIDNamePair<string,string>,short,string>
    {
        public byte BaseUnit
        {
            get;set;
        }

        public string OwnerClientID
        {
            get;
            set;
        }

        public Enum.CurrencyType CurrencyType
        {
            get;
            set;
        }

        public Token[] Denominations
        {
            get;
            set;
        }

        public float[] SubUnits
        {
            get;
            set;
        }

        public string PluralName
        {
            get;
            set;
        }

        public string SymbolUrl
        {
            get;
            set;
        }

        public Token[] GetDenomination(Common.IIDNamePair<string, string> dType)
        {
            throw new NotImplementedException();
        }

        public string ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
