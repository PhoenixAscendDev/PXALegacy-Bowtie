using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public delegate void ProcessDewdrop(IDewdrop dewdrop, string playerID, object value);
    public delegate void ProcessGameCommand(IGameCommand command, IGameSession session);
    public interface IGameEngine<TGameSession>
        where TGameSession : IGameSession, new()
    {
        #region Events
        event Action<IGameEngine<TGameSession>, TGameSession> SessionStarted;
        event Action<IGameEngine<TGameSession>, TGameSession> SessionStopped;
        event Action<IGameEngine<TGameSession>, IAchievement, IBowtiePlayer, int> AchievementUnlocked;
        event Action<IGameEngine<TGameSession>, IAchievement, IBowtiePlayer, int, IEnumerable<Enum.AchievementFlag>> AchievementUpdated;
        event Action<IGameEngine<TGameSession>, JB2.Economy.ITreasuryNote, IBowtiePlayer> jBeanAwarded;
        event Action<IGameEngine<TGameSession>, IBowtiePlayer, int> PlayerAdded;
        event Action<IGameEngine<TGameSession>, IBowtiePlayer, int> PlayerDropped;
        event Action<IGameEngine<TGameSession>, IDewdrop, IBowtiePlayer> DewdropIssued;
        event Action<IGameEngine<TGameSession>, IGameCommand> GameCommandIssued;
        #endregion Events;

        #region Getters
        IEnumerable<IPlayerDewdrop> GetDewdrops();

        IEnumerable<IBowtiePlayer> GetPlayers();

        TGameSession FindSessionByPlayer(IBowtiePlayer player);

        TGameSession FindSessionByPlayerID(string playerID);


        IEnumerable<TGameSession> GetSessions();

        IApplication GetApplication();

        #endregion Getters

        #region Methods

        TGameSession StartNewSession(IApplication application);

        void EndSession(TGameSession session);

        void ProcessDewdrops(ProcessDewdrop processDewdrop);
        void ProcessGameCommands(ProcessGameCommand processCommand);
        void AddPlayer(string sessionID, int seat, IBowtiePlayer player);
        void AddPlayer(string sessionID, IBowtiePlayer player);

        void AddDewDrop(string dewdropID, string playerID, object value);
        void RemovePlayer(string sessionID, int seat);
        void RemovePlayer(string sessionID, IBowtiePlayer player);

        void Sync();

        #endregion Methods

    }
}
