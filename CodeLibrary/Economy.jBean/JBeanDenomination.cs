using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace JB2.Economy
{
    public class JBeanDenomination : JB2.Common.IDNamePair, IDenomination
    {
        protected Enum.JBeanTokenType _type;
        

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
                return JB2.Settings.Jbean.GetTokenImageFront(this._type);
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
                return JB2.Settings.Jbean.GetTokenImageBack(this._type);
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
                return JB2.Settings.Jbean.GetSetting(JbeanSettingName.CurrencyID).ToString();
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
                return JB2.Settings.Jbean.GetTokenValue(this._type);
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


        public override string Name
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
