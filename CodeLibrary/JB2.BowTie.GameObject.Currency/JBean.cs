using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class JBean : ICurrency
    {
        public byte BaseUnit
        {
            get
            {
                return 1;             
            }
            set {}
        }

        public string OwnerClientID
        {
            get;set;
            
        }

        public Enum.CurrencyType CurrencyType
        {
            get { return Enum.CurrencyType.NonDecimal; }
            set { }
        }

        public IDenomination[] Denominations
        {
            get;set;
        }

        public float[] SubUnits
        {
            get;set;
        }

        public string PluralName
        {
            get;set;
        }

        public string SymbolUrl
        {
            get;set;
        }

        public string ID
        {
            get;set;
        }

        public string Name
        {
            get;set;
        }
    }
}
