using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class JBeanService
    {
        #region Fields
        protected IWalletRepository _walletrepo;
        protected IUnitOfWork _uofw;
        #endregion Fields

        #region Constructors
        public JBeanService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }
        public JBeanService(IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;
            _walletrepo = _uofw.WalletRepository;
        }

        public JBeanService(IWalletRepository repo) : this()
        {
            _walletrepo = repo;
        }

        #endregion Constructors

        public void AddJBeansToWallet(IBowtiePlayer player, IApplication app, int amount )
        {
            var wallet = getWallet(player, app);                
        }

        private  JB2.Economy.ITreasuryNote GetJBeansFromTreasury(IApplication app, long amount)
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
                return note;
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
