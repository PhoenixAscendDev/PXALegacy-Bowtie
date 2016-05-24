using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IGameEngine
    {
        #region Events
        event Action<IGameEngine,IGameSession> SessionStarted;
        event Action<IGameEngine,IGameSession> SessionStopped;
        event Action<IGameEngine,IAchievement, IBowtiePlayer,int> AchievementUnlocked;
        event Action<IGameEngine,IAchievement, IBowtiePlayer,int, IEnumerable<Enum.AchievementFlag>> AchievementUpdated;
        event Action<IGameEngine,JB2.Economy.ITreasuryNote, IBowtiePlayer> jBeanAwarded;  
        event Action<IGameEngine,IBowtiePlayer, int> PlayerAdded;
        event Action<IGameEngine,IBowtiePlayer, int> PlayerDropped;
        event Action<IGameEngine,IDewdrop,IBowtiePlayer> DewdropIssued;
        event Action<IGameEngine,IGameCommand> GameCommandIssued;
        #endregion Events;

        #region Getters
        IEnumerable<IPlayerDewdrop> GetDewdrops();

        IEnumerable<IBowtiePlayer> GetPlayers();

        IGameSession FindSessionByPlayer(IBowtiePlayer player);

        IGameSession FindSessionByPlayerID(string playerID);


        IEnumerable<IGameSession> GetSessions();

        IApplication GetApplication();

        #endregion Getters

        #region Methods

        IGameSession StartNewSession(IApplication application);

        void EndSession(IGameSession session);

        void RegisterDewdrop(string dewdropID, string playerID, object value);

        void ProcessGameCommand(IGameCommand command);
        void AddPlayer(string sessionID, int seat, IBowtiePlayer player);
        void AddPlayer(string sessionID,IBowtiePlayer player);
        void RemovePlayer(string sessionID,int seat);
        void RemovePlayer(string sessionID,IBowtiePlayer player);

        #endregion Methods

    }
}
