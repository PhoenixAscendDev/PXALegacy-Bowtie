using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Economy
{
    public class JBeanDenomination : IDenomination
    {
        protected Enum.JBeanTokenType _type;
        protected string _name;
        protected string _id;

        #region Constructor

        public JBeanDenomination(): this(Enum.JBeanTokenType.Kidney)
        {

        }

        public JBeanDenomination(Enum.JBeanTokenType type)
        {
            _type = type;
        }
        #endregion Constructor




        public string ImageFrontUri
        {
            get
            {
                return JB2.Bowtie.Jbean.GetTokenImageFront(this._type);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string ImageBackUri
        {
            get
            {
                return JB2.Bowtie.Jbean.GetTokenImageBack(this._type);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string CurrencyID
        {
            get
            {
                return JB2.Bowtie.Jbean.GetSetting(JbeanSettingName.CurrencyID).ToString();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public int UnitMultiplier
        {
            get
            {
                return JB2.Bowtie.Jbean.GetTokenValue(this._type);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public bool isSubUnit
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotSupportedException();
            }
        }


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

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return System.Enum.GetName(typeof(Enum.JBeanTokenType), this._type);
                else
                    return _name;
            }
            set
            {
                _name = value;
            }
        }

        public Enum.JBeanTokenType DenominationType
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }


        public static implicit operator int(JBeanDenomination r)
        {
            return (int)r.UnitMultiplier;
        }


    }
}
