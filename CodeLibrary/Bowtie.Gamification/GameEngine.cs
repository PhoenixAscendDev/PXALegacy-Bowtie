using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Economy;

namespace JB2.Bowtie
{

    public abstract class GameEngine<TSession> : IGameEngine<TSession>
        where TSession: IGameSession, new()
    {
        #region Fields

        protected IDictionary<string, TSession> _sessions;
        protected IApplication _application;
        protected JB2.Common.BaseCollection<IPlayerDewdrop> _dewdrops;

        #endregion Fields

        #region Players
        public virtual void AddPlayer(string sessionID, IBowtiePlayer player)
        {
            var session = _sessions[sessionID];
            session.AddPlayer(player);
        }
        public abstract void AddPlayer(string sessionID, int seat, IBowtiePlayer player);
        public abstract IEnumerable<IBowtiePlayer> GetPlayers();
        public abstract void RemovePlayer(string sessionID, IBowtiePlayer player);
        public abstract void RemovePlayer(string sessionID, int seat);

        #endregion Players

        #region Session
        public abstract void EndSession(TSession session);
        public abstract TSession FindSessionByPlayer(IBowtiePlayer player);
        public abstract TSession FindSessionByPlayerID(string playerID);

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

        public abstract void AddDewDrop(string dewdropID, string playerID, object value);


        #endregion Dewdrops


        #region Process Delgates

        public void ProcessDewdrops(ProcessDewdrop processDewdrop)
        {
            foreach(var pdew in _dewdrops)
            {
                var dewdrop = JB2.Settings.Bowtie.UnitOfWork.DewdropRepository.GetById(pdew.GetDewdropID());
                processDewdrop(dewdrop, pdew.GetPlayerID(), pdew.GetValue());

            }
        }

        public void ProcessGameCommands(ProcessGameCommand processCommand)
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





        public event Action<IGameEngine<TSession>, IAchievement, IBowtiePlayer, int> AchievementUnlocked;
        public event Action<IGameEngine<TSession>, IAchievement, IBowtiePlayer, int, IEnumerable<AchievementFlag>> AchievementUpdated;
        public event Action<IGameEngine<TSession>, IDewdrop, IBowtiePlayer> DewdropIssued;
        public event Action<IGameEngine<TSession>, IGameCommand> GameCommandIssued;
        public event Action<IGameEngine<TSession>, Economy.ITreasuryNote, IBowtiePlayer> jBeanAwarded;
        public event Action<IGameEngine<TSession>, IBowtiePlayer, int> PlayerAdded;
        public event Action<IGameEngine<TSession>, IBowtiePlayer, int> PlayerDropped;
        public event Action<IGameEngine<TSession>, TSession> SessionStarted;
        public event Action<IGameEngine<TSession>, TSession> SessionStopped;

        protected virtual void OnSessionStart(IGameSession session,DateTime start)
        {
            if (SessionStarted != null)
                SessionStarted(this, (TSession)session);
        }

        protected virtual void OnSessionStop(IGameSession session, DateTime end)
        {
            if (SessionStopped != null)
                SessionStopped(this, (TSession)session);
        }

        protected virtual void OnSessionPlayerAdd(IGameSession session, IBowtiePlayer player, int seat)
        {
            if (PlayerAdded != null)
                PlayerAdded(this, player, seat);
        }

        protected virtual void OnSessionPlayerRemove(IGameSession session, IBowtiePlayer player, int seat)
        {
            if (PlayerDropped != null)
                PlayerDropped(this, player, seat);
        }

        
    }
}
