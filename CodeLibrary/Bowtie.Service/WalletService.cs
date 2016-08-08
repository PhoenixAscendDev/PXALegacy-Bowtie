using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class WalletService
    {
        #region Fields
        protected IWalletRepository _walletrepo;
        protected IUnitOfWork _uofw;
        #endregion Fields

        #region Constructors
        public WalletService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }
        public WalletService(IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;
            _walletrepo = _uofw.WalletRepository;
        }

        public WalletService(IWalletRepository repo) : this()
        {
            _walletrepo = repo;
        }

        #endregion Constructors

        public void AddJBeansToWallet(IBowtiePlayer player, IApplication app, int amount)
        {
            var wallet = getWallet(player, app);

            AddJBeansToWallet(wallet, amount);
        }

        public JB2.Economy.ITreasuryNote AddJBeansToWallet(IWallet wallet, int amount)
        {
            var player = _uofw.PlayerRepository.GetById(wallet.Owner.ID);
            var app = _uofw.ApplicationRepository.GetById(wallet.GetApplicationID());

            var treasuryNote = getJBeansFromTreasury(app, amount);

            if (treasuryNote != null && treasuryNote.Amount == amount)
            {
                wallet.AddTreasuryNote(treasuryNote);
            }

            _walletrepo.Insert(wallet);

            return treasuryNote;

        }

        //public void RemoveJBeansToWaller(IBowtiePlayer player, IApplication app, int amount)
        //{
        //    var wallet = getWallet(player, app);

        //    RemoveJBeansToWallet(wallet, amount);
        //}
        public void RemoveJBeansToWallet(IWallet wallet, int amount)
        {
            var player = _uofw.PlayerRepository.GetById(wallet.Owner.ID);
            var app = _uofw.ApplicationRepository.GetById(wallet.GetApplicationID());

            //we don't allow negative accounts
            var amountInWallet = wallet.JBeanTotal;
            if(amountInWallet >= amount)
            {
                wallet.RemoveAmount(JB2.Settings.Jbean.Factory.Currencies[0], amount);
            }
            _walletrepo.Insert(wallet);

        }

        public IWallet RetrieveWalletByPlayer(IBowtiePlayer player,IApplication app)
        {
            var wallet = getWallet(player, app);

            return wallet;
        }

        public IWallet GenerateWalletForPlayer(IBowtiePlayer player, IApplication app)
        {
            IWallet wallet = null;

            string id = "w-" + JB2.Common.NewID.ShortGuid();
            wallet = new PlayerWallet(id, player.GetPlayerID(), new Economy.JBeanCollection());
            wallet.ApplicationID = app.GetID();
            _walletrepo.Insert(wallet);

            return wallet;
        }

        private  JB2.Economy.JbeanTreasuryNote getJBeansFromTreasury(IApplication app, long amount)
        {
            // var settings = _uofw.JbeanRepository.GetApplicationSettings(app);

            var requestKey = _uofw.ApplicationRepository.GetTreasuryRequestKey(app.GetID(), "jBean");

            var request = new JB2.Economy.jBeanRequest();
            request.RequestDate = System.DateTime.Now;
            request.Requestor = app;
            request.VerificationKey = requestKey;
            request.Amount = amount;

            var note = JB2.Settings.Jbean.Factory.Treasury.IssueNote(request);

            if (note != null && note.Amount == amount)
            {
                return (JB2.Economy.JbeanTreasuryNote)note;
            }
            else
                return null;         
        }

        private IWallet getWallet(IBowtiePlayer player, IApplication app)
        {
            var wallet = _walletrepo.GetByPlayerAndApplication(player.GetPlayerID(), app.GetID());

            if(wallet == null)
            {
                wallet = GenerateWalletForPlayer(player, app);           
            }

            return wallet;

        }


    }
}
