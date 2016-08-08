using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Economy;

namespace JB2.Bowtie
{
    
    public abstract class GameEngine<TSession, TPlayer, TID> : IGameEngine<TSession,TPlayer,TID>
        where TSession: IGameSession<TPlayer,TID>, new()
        where TPlayer :  JB2.Bowtie.IPlayerable<TID>
        where TID : IComparable
    {
        #region Fields

        protected IDictionary<string, TSession> _sessions;
        protected IApplication _application;
        protected JB2.Common.BaseCollection<IPlayerDewdrop> _dewdrops;

        #endregion Fields

        #region Players
        public virtual void AddPlayer(string sessionID, TPlayer player)
        {
            var session = _sessions[sessionID];
            session.AddPlayer(player);
        }
        public abstract void AddPlayer(string sessionID, int seat, TPlayer player);
        public abstract IEnumerable<TPlayer> GetPlayers();
        public abstract void RemovePlayer(string sessionID, TPlayer player);
        public abstract void RemovePlayer(string sessionID, int seat);

        #endregion Players

        #region Session
        public abstract void EndSession(TSession session);
        public abstract TSession FindSessionByPlayer(TPlayer player);
        public abstract TSession FindSessionByPlayerID(TID playerID);

        public virtual IEnumerable<TSession> GetSessions()
        {
            return _sessions.Values.ToList();
        }
        public virtual TSession StartNewSession(IApplication application)
        {
            _application = application;
            var session = new TSession();
           
            session.Started += OnSessionStart;
            session.ManuallyStopped += OnSessionStop;
            session.PlayerAdded += OnSessionPlayerAdd;
            session.PlayerRemoved += OnSessionPlayerRemove;

            session.Start();

            return session;
        }

        #endregion Session

        #region Application
        public virtual IApplication GetApplication()
        {
            return _application;
        }

        #endregion Application

        #region Dewdrops

        public abstract IEnumerable<IPlayerDewdrop> GetDewdrops();

        public virtual void AddDewDrop(string dewdropID, TPlayer player, object value)
        {
            var dewdrop = JB2.Settings.Bowtie.UnitOfWork.DewdropRepository.GetById(dewdropID);

            var btplayer = getPlayer(player);

            if (validateDewDrop(dewdrop))
            {

                //add jBeans to Wallet
                int amount = dewdrop.GetjBeanCost();

                if (amount > 0)
                {
                    var tNote = addAmountToWallet(JB2.Settings.Jbean.Factory.Currencies[0], amount, player);
                    var wallet = new JB2.Bowtie.Service.WalletService().RetrieveWalletByPlayer(btplayer, this.GetApplication());
                    if (TreasuryNoteAdded != null)
                        TreasuryNoteAdded(this, wallet, player, tNote);
                    if (jBeanAwarded != null)
                        jBeanAwarded(this, tNote, player);
                }

                //add dewdrop
                var dewdropService = new JB2.Bowtie.Service.DewdropService();
                PlayerDewdrop pdew = dewdropService.GenerateNewPlayerDewdrop(btplayer, dewdrop, value.ToString());
                dewdropService.Save(pdew);
                this._dewdrops.Add(pdew);


                //fire event
                if (DewdropIssued != null)
                    DewdropIssued(this, dewdrop, (TPlayer)player);
            }

        }

        private bool validateDewDrop(IDewdrop dewdrop)
        {
            return true;
        }

        private IBowtiePlayer getPlayer(TPlayer player)
        {
            if (player is IBowtiePlayer)
                return (IBowtiePlayer)player;
            else
            {
                var playerService = new JB2.Bowtie.Service.PlayerService();
                var bowtiePlayer = playerService.RetrieveById(player.GetPlayerID().ToString());

                return bowtiePlayer;
            }
        }

        private JB2.Economy.ITreasuryNote addAmountToWallet(ICurrency currency, decimal amount, TPlayer player)
        {
            JB2.Economy.ITreasuryNote tNote = null;

            var btplayer = getPlayer(player);

            var walletService = new JB2.Bowtie.Service.WalletService();
            var wallet = walletService.RetrieveWalletByPlayer(btplayer, this.GetApplication());

            if(currency.GetID() == JB2.Configuration.GetjBeanCurrencyID() && wallet != null)
            {
                int jBeanAmount = (JBeanBag)amount;
                if (jBeanAmount > 0)
                    tNote = walletService.AddJBeansToWallet(wallet, jBeanAmount);
                else
                    tNote =  null;
            }

            return tNote;

            //if(currency.GetID() ==  )
        }

        private void removeAmountToWallet(ICurrency currency, decimal amount, TPlayer player)
        {
            var btplayer = getPlayer(player);

            var walletService = new JB2.Bowtie.Service.WalletService();
            var wallet = walletService.RetrieveWalletByPlayer(btplayer, this.GetApplication());

            if (currency.GetID() == JB2.Configuration.GetjBeanCurrencyID() && wallet != null)
            {
                int jBeanAmount = (JBeanBag)amount;
                walletService.RemoveJBeansToWallet(wallet, jBeanAmount);
            }
        }

        #endregion Dewdrops

        #region Signin

        public virtual void SignInPlayer(TPlayer player)
        {
            var dewID = JB2.Configuration.GetAppSetting("JB2:dewdrop:signinout");
            AddDewDrop(dewID, player, "IN");

            if (PlayerSignedIn != null)
                PlayerSignedIn(this, player, DateTime.Now);

        }

        public virtual void SignOutPlayer(TPlayer player)
        {
            var dewID = JB2.Configuration.GetAppSetting("JB2:dewdrop:signinout");
            AddDewDrop(dewID, player, "OUT");

            if (PlayerSignedOut != null)
                PlayerSignedOut(this, player, DateTime.Now);
        }


        #endregion SignOut


        #region Process Delgates

        public void ProcessDewdrops(ProcessDewdrop processDewdrop)
        {
            var dewdropService = new JB2.Bowtie.Service.DewdropService();
            foreach(var pdew in _dewdrops)
            {
                var dewdrop = dewdropService.RetrieveById(pdew.GetDewdropID());
                processDewdrop(dewdrop, pdew.GetPlayerID(), pdew.GetValue());

            }
        }

        public void ProcessGameCommands(ProcessGameCommand<TPlayer,TID> processCommand)
        {
            foreach(var session in _sessions.Values)
            {
                foreach(var command in session.GetGameCommands())
                {
                    processCommand(command, session);
                }
            }
        }

        #endregion Process Delgates


        public abstract void Sync();


        public event Action<IGameEngine<TSession, TPlayer, TID>, IAchievement, TPlayer, int> AchievementUnlocked;
        public event Action<IGameEngine<TSession, TPlayer, TID>, IAchievement, TPlayer, int, IEnumerable<AchievementFlag>> AchievementUpdated;
        public event Action<IGameEngine<TSession, TPlayer, TID>, IDewdrop, TPlayer> DewdropIssued;
        public event Action<IGameEngine<TSession, TPlayer, TID>, IGameCommand> GameCommandIssued;
        public event Action<IGameEngine<TSession, TPlayer, TID>, Economy.ITreasuryNote, TPlayer> jBeanAwarded;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, int> PlayerAdded;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, int> PlayerDropped;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TSession> SessionStarted;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TSession> SessionStopped;
        public event Action<IGameEngine<TSession, TPlayer, TID>, IWallet, TPlayer, JB2.Economy.ITreasuryNote> TreasuryNoteAdded;
        public event Action<IGameEngine<TSession, TPlayer, TID>, IAchievement, TPlayer> AchievementEarned;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, DateTime> PlayerSignedIn;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, DateTime> PlayerSignedOut;

        protected virtual void OnSessionStart(IGameSession<TPlayer, TID> session,DateTime start)
        {
            
            if (SessionStarted != null)
                SessionStarted(this, (TSession)session);
        }

        protected virtual void OnSessionStop(IGameSession<TPlayer, TID> session, DateTime end)
        {
            if (SessionStopped != null)
                SessionStopped(this, (TSession)session);
        }

        protected virtual void OnSessionPlayerAdd(IGameSession<TPlayer, TID> session, TPlayer player, int seat)
        {
            if (PlayerAdded != null)
                PlayerAdded(this, player, seat);
        }

        protected virtual void OnSessionPlayerRemove(IGameSession<TPlayer, TID> session, TPlayer player, int seat)
        {
            if (PlayerDropped != null)
                PlayerDropped(this, player, seat);
        }

        
    }
}
