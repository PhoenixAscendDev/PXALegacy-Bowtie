using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using JB2.Economy;



namespace JB2.Bowtie
{
    public class PlayerWallet : JB2.Common.IDValue<string>, IWallet
    {
        #region Fields

        protected string _appID;
        protected JBeanCollection _jbeanTokens;
        protected string _playerID;

        #endregion Fields
        public string ApplicationID
        {
            get
            {
                return _appID;
            }

            set
            {
                _appID = value;
            }
        }   

        public JBeanBag JBeanTotal
        {
            get
            {
                return _jbeanTokens;
                
            }
        }

        public IPerson<string> Owner
        {
            get
            {
                var p = JB2.Settings.Bowtie.UnitOfWork.PlayerRepository.GetById(_playerID);
                return p;
            }
        }

        public void AddAmount(ICurrency currency, double quantity)
        {
            throw new NotImplementedException();
        }

        public double CurrencyTotal(ICurrency currency)
        {
            throw new NotImplementedException();
        }

        public string GetApplicationID()
        {
            return _appID;
        }

        public override string GetID()
        {
            return base.ID;
        }

        public void RemoveAmount(ICurrency currency, double quantity)
        {
            throw new NotImplementedException();
        }
    }
}
