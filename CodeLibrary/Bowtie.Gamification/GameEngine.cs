using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Economy;

namespace JB2.Bowtie
{
    public abstract class GameEngine<TSession> : IGameEngine
        where TSession: IGameSession, new()
    {


        #region Fields

        protected IDictionary<string, IGameSession> _sessions;
        protected IApplication _application;

        #endregion Fields

        public virtual void AddPlayer(string sessionID, IBowtiePlayer player)
        {
            var session = _sessions[sessionID];
            session.AddPlayer(player);
        }
        public abstract void AddPlayer(string sessionID, int seat, IBowtiePlayer player);
        public abstract void EndSession(IGameSession session);
        public abstract IGameSession FindSessionByPlayer(IBowtiePlayer player);
        public abstract IGameSession FindSessionByPlayerID(string playerID);
        public virtual IApplication GetApplication()
        {
            return _application;
        }
        public abstract IEnumerable<IPlayerDewdrop> GetDewdrops();
        public abstract IEnumerable<IBowtiePlayer> GetPlayers();
        public virtual IEnumerable<IGameSession> GetSessions()
        {
            return _sessions.Values.ToList();
        }
        public abstract void ProcessGameCommand(IGameCommand command);
        public abstract void RegisterDewdrop(string dewdropID, string playerID, object value);
        public abstract void RemovePlayer(string sessionID, IBowtiePlayer player);
        public abstract void RemovePlayer(string sessionID, int seat);
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


        public event Action<IGameEngine, IAchievement, IBowtiePlayer, int> AchievementUnlocked;
        public event Action<IGameEngine, IAchievement, IBowtiePlayer, int, IEnumerable<AchievementFlag>> AchievementUpdated;
        public event Action<IGameEngine, IDewdrop, IBowtiePlayer> DewdropIssued;
        public event Action<IGameEngine, IGameCommand> GameCommandIssued;
        public event Action<IGameEngine, Economy.ITreasuryNote, IBowtiePlayer> jBeanAwarded;
        public event Action<IGameEngine, IBowtiePlayer, int> PlayerAdded;
        public event Action<IGameEngine, IBowtiePlayer, int> PlayerDropped;
        public event Action<IGameEngine, IGameSession> SessionStarted;
        public event Action<IGameEngine, IGameSession> SessionStopped;

        protected virtual void OnSessionStart(IGameSession session,DateTime start)
        {
            if (SessionStarted != null)
                SessionStarted(this, session);
        }

        protected virtual void OnSessionStop(IGameSession session, DateTime end)
        {
            if (SessionStopped != null)
                SessionStopped(this, session);
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
