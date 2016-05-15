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

            var treasuryNote = getJBeansFromTreasury(app, amount);

            if(treasuryNote.Amount == amount)
            {
                wallet.AddTreasuryNote(treasuryNote);
            }
        }

        private  JB2.Economy.JbeanTreasuryNote getJBeansFromTreasury(IApplication app, long amount)
        {
            var settings = _uofw.JbeanRepository.GetApplicationSettings(app);

            if (!settings.CanRequest)
                throw new JB2.Economy.Exceptions.IssueJBeanProhibited();

            var request = new JB2.Economy.TreasuryRequest();
            request.RequestDate = System.DateTime.Now;
            request.Requestor = app;
            request.VerificationKey = settings.RequestValidationKey;
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
                string id = "w-" + JB2.Common.NewID.ShortGuid();
                wallet = new PlayerWallet(id, player.GetPlayerID(), new Economy.JBeanCollection());
                wallet.ApplicationID = app.GetID();
                _walletrepo.Insert(wallet);
            }

            return wallet;

        }
    }
}
