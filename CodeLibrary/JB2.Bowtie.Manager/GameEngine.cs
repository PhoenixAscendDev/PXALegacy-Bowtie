using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;
using JB2.Economy;

namespace JB2.Bowtie
{

    public abstract class GameEngine<TSession, TPlayer, TID> : IGameEngine<TSession, TPlayer, TID>
        where TSession : IGameSession<TPlayer, TID>, new()
        where TPlayer : JB2.Bowtie.IPlayerable<TID>
        where TID : IComparable
    {
        #region Fields

        protected IDictionary<string, TSession> _sessions;
        protected IApplication _application;
        protected JB2.Common.BaseCollection<IPlayerDewdrop> _dewdrops;
        protected JB2.Common.BaseCollection<IGameCommand> _commands;
        

        #endregion Fields


        #region Constructors
        public GameEngine()
        {
            _dewdrops = new Common.BaseCollection<IPlayerDewdrop>();
            _commands = new Common.BaseCollection<IGameCommand>();
            _sessions = new Dictionary<string, TSession>();
            DewdropIssued += OnDewdropIssued;
        }

        #endregion Constructors

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
            session.CommandAdded += OnGameCommandIssue;
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

        public abstract int GetDewdropCount(TPlayer player, string Dewdropcode);

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
                dewdropService.Save(pdew,false);

                btplayer.AddDewDrop(dewdrop);

                this._dewdrops.Add(pdew);

               



                //fire event
                if (DewdropIssued != null)
                    DewdropIssued(this, pdew, (TPlayer)player);
            }

        }

        private bool validateDewDrop(IDewdrop dewdrop)
        {
            return true;
        }

        private JB2.Economy.ITreasuryNote addAmountToWallet(ICurrency currency, decimal amount, TPlayer player)
        {
            JB2.Economy.ITreasuryNote tNote = null;

            var btplayer = getPlayer(player);

            var walletService = new JB2.Bowtie.Service.WalletService();
            var wallet = walletService.RetrieveWalletByPlayer(btplayer, this.GetApplication());

            if (currency.GetID() == JB2.Configuration.GetjBeanCurrencyID() && wallet != null)
            {
                int jBeanAmount = (JBeanBag)amount;
                if (jBeanAmount > 0)
                    tNote = walletService.AddJBeansToWallet(wallet, jBeanAmount);
                else
                    tNote = null;
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

        #region Game Command
        public void AddGameCommand(string commandCode, string sessionID, TPlayer issuedPlayer, TPlayer affectedPlayer, RunGameCommand<TPlayer, TID> runCommand)
        {
            

            //create GameCommand
            var gamecommandService = new JB2.Bowtie.Service.GameCommandService();
            var gc = gamecommandService.New(commandCode, this._application.GetID(), sessionID);
            gc.AffectedPlayerID = affectedPlayer.GetPlayerID().ToString();
            gc.IssuedPlayerID = issuedPlayer.GetPlayerID().ToString();

            //add it to session
            var session = _sessions[sessionID];
            if (session != null)
            {
                session.AddGameCommand(gc);
                _commands.Add(gc);
            }
            //run command
            runCommand(gc, session);
        }


        #endregion Game Command


        #region Process Delgates

        public void ProcessDewdrops(ProcessDewdrop processDewdrop)
        {
            var dewdropService = new JB2.Bowtie.Service.DewdropService();
            foreach (var pdew in _dewdrops)
            {
                var dewdrop = dewdropService.RetrieveById(pdew.GetDewdropID());
                processDewdrop(dewdrop, pdew.GetPlayerID(), pdew.GetValue());

            }
        }

        public void ProcessGameCommands(RunGameCommand<TPlayer, TID> processCommand)
        {
            foreach (var session in _sessions.Values)
            {
                foreach (var command in session.GetGameCommands())
                {
                    processCommand(command, session);
                }
            }
        }

        #endregion Process Delgates


        public abstract void Sync();


        #region Events
        public event Action<IGameEngine<TSession, TPlayer, TID>, IAchievement, TPlayer, int> AchievementUnlocked;
        public event Action<IGameEngine<TSession, TPlayer, TID>, IAchievement, TPlayer, int, IEnumerable<AchievementFlag>> AchievementUpdated;
        public event Action<IGameEngine<TSession, TPlayer, TID>, IPlayerDewdrop, TPlayer> DewdropIssued;
        public event Action<IGameEngine<TSession, TPlayer, TID>, IGameCommand> GameCommandIssued;
        public event Action<IGameEngine<TSession, TPlayer, TID>, Economy.ITreasuryNote, TPlayer> jBeanAwarded;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, int> PlayerAdded;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, int> PlayerDropped;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TSession> SessionStarted;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TSession> SessionStopped;
        public event Action<IGameEngine<TSession, TPlayer, TID>, IWallet, TPlayer, JB2.Economy.ITreasuryNote> TreasuryNoteAdded;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, DateTime> PlayerSignedIn;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, DateTime> PlayerSignedOut;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, IMetaData> PlayerDataChanged;
        public event Action<IGameEngine<TSession, TPlayer, TID>, TPlayer, IPlayerInventoryItem> PlayerInventoryChanged;

        #endregion Events


        #region Event Actions
        protected virtual void OnGameCommandIssue(IGameSession<TPlayer, TID> session, IGameCommand command)
        {
            if (GameCommandIssued != null)
                GameCommandIssued(this, command);
        }

        protected virtual void OnPlayerSignIn(IGameEngine<TSession, TPlayer, TID> engine, TPlayer player, DateTime signinDate)
        {
            if (PlayerSignedIn != null)
                PlayerSignedIn(engine, player, signinDate);

            JB2.Events.Bowtie.OnPlayerSignedIn(getPlayer(player), engine.GetApplication(), signinDate);
        }


        protected virtual void OnSessionStart(IGameSession<TPlayer, TID> session, DateTime start)
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

        protected virtual void OnDewdropIssued(IGameEngine<TSession, TPlayer, TID> engine, IPlayerDewdrop dewdrop, TPlayer player)
        {
            

        }

        protected virtual void OnPlayerInventoryChanged(IGameEngine<TSession, TPlayer, TID> engine, TPlayer player, IPlayerInventoryItem item)
        {
            if (PlayerDataChanged != null)
                PlayerInventoryChanged(engine, player, item);
        }

        protected virtual void OnPlayerDataChanged(IGameEngine<TSession, TPlayer, TID> engine, TPlayer player, IMetaData metadata)
        {
            if (PlayerDataChanged != null)
                PlayerDataChanged(engine, player, metadata);
        }

        protected void CheckAchievement(TPlayer player, IPlayerDewdrop dewdrop)
        {
            //determine the achievements
            var aservice = new JB2.Bowtie.Service.AchievementService();
            var palist = aservice.RetrievePlayerAchievement(dewdrop.GetPlayerID(), this.GetApplication().GetID());

            foreach (var pa in palist)
            {
                if(pa.isAchieved == false)
                {
                    var a = aservice.RetrieveById(pa.AchievementID);
                    if (a.DewdropTriggers.Contains(dewdrop.GetDewdropID()))
                    {
                        var newpa = aservice.CalculateAchievement(getPlayer(player), a.GetID(), dewdrop);
                        aservice.Save(newpa);
                        if (pa.isAchieved == true && AchievementUnlocked != null)
                            AchievementUnlocked(this, a, player, 0);
                     }
                }

                

            }

        }

        #endregion Event Actions

        #region Helper Methods
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
        #endregion Helper Methods




    }
}
