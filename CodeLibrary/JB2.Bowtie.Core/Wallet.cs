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
        protected JBeanCollection _treasuryNotes;
        protected IEnumerable<JB2.Bowtie.WalletReceipt> _receipts;
        protected Dictionary<string, double> _amounts;
        protected string _id;
        
        protected string _playerID;

        #endregion Fields

        #region Constructor

        public override string ID
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

        public override string GetID()
        {
            return _id;
        }

        public PlayerWallet(string id, string playerID, JBeanCollection jbeanTreasuryNotes)
        {
            _id = id;
            _treasuryNotes = jbeanTreasuryNotes;
            _playerID = playerID;
            _amounts = new Dictionary<string, double>();
            _amounts.Add(JB2.Settings.Jbean.Factory.Currencies[0].ID, (double)jbeanTreasuryNotes);
            _receipts = new List<WalletReceipt>();
        }

        #endregion Constructor
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
                int noteAmount =  (JBeanBag)_treasuryNotes;
                int totalWithdraw = 0;

                foreach(var r in _receipts.ToList().FindAll(x => x.TransactionType == WalletTransationType.Withdraw))
                {
                    totalWithdraw = totalWithdraw + (int)r.Amount;
                }

                return noteAmount - totalWithdraw;              
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
            if (!_amounts.ContainsKey(currency.ID))
                _amounts.Add(currency.ID, 0);

            var receipt = new WalletReceipt();
            receipt.TransactionType = WalletTransationType.Deposit;
            receipt.CurrencyID = currency.ID;
            receipt.TransationID = "wt-" + JB2.Common.NewID.ShortGuid();
            receipt.Amount = quantity;
            receipt.TransactionDate = DateTime.Now;
            _receipts.ToList().Add(receipt);

            _amounts[currency.ID] = _amounts[currency.ID] + quantity;
        }

        public double CurrencyTotal(ICurrency currency)
        {
            if (currency.ID == JB2.Settings.Jbean.Factory.Currencies[0].ID)
                return Convert.ToDouble((int)this.JBeanTotal);
            else if (_amounts.ContainsKey(currency.ID))
                return _amounts[currency.ID];
            else
                return 0;
        }

        public string GetApplicationID()
        {
            return _appID;
        }


        public void RemoveAmount(ICurrency currency, double quantity)
        {
            if(!_amounts.ContainsKey(currency.ID))
                _amounts.Add(currency.ID, 0);

            var receipt = new WalletReceipt();
            receipt.TransactionType = WalletTransationType.Withdraw;
            receipt.TransactionDate = DateTime.Now;
            receipt.CurrencyID = currency.ID;
            receipt.TransationID = "wt-" + JB2.Common.NewID.ShortGuid();
            receipt.Amount = quantity;
            _receipts.ToList().Add(receipt);

            _amounts[currency.ID] = _amounts[currency.ID] - quantity;
        }

        public IEnumerable<jBeanToken>  GetJBeanTokens()
        {
            return null;
        }

        public bool AddTreasuryNote(JbeanTreasuryNote note)
        {
            var result = _treasuryNotes.Add(note);

            if (result)
                this.AddAmount(JB2.Settings.Jbean.Factory.Currencies[0], note.Amount);
                //_amounts[JB2.Settings.Jbean.Factory.Currencies[0].ID] = _amounts[JB2.Settings.Jbean.Factory.Currencies[0].ID] + note.Amount;

            return result;
        }
        
        public IEnumerable<WalletReceipt> GetReceipts()
        {
            return _receipts;
        }

        public string GetPlayerID()
        {
            return _playerID;
        }
    }
}
