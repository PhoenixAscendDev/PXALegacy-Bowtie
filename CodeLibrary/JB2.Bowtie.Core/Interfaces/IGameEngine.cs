using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public delegate void ProcessDewdrop(IDewdrop dewdrop, string playerID, object value);
    public delegate void ProcessGameCommand<TPlayer,TID>(IGameCommand command, IGameSession<TPlayer,TID> session) 
        where TPlayer: JB2.Identity.IPlayerable<TID> 
        where TID : IComparable;

    public interface IGameEngine<TGameSession> : IGameEngine<TGameSession,IBowtiePlayer,string>
        where TGameSession: IGameSession<IBowtiePlayer,string>,new()
    {

    }
    public interface IGameEngine<TGameSession,TPlayer,TID>
        where TGameSession : IGameSession<TPlayer,TID>, new()
        where TPlayer :  JB2.Identity.IPlayerable<TID>
        where TID : IComparable
    {
        #region Events
        event Action<IGameEngine<TGameSession,TPlayer,TID>, TGameSession> SessionStarted;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, TGameSession> SessionStopped;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, IAchievement, TPlayer, int> AchievementUnlocked;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, IAchievement, TPlayer, int, IEnumerable<Enum.AchievementFlag>> AchievementUpdated;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, JB2.Economy.ITreasuryNote, TPlayer> jBeanAwarded;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, TPlayer, int> PlayerAdded;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, TPlayer, int> PlayerDropped;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, IDewdrop, TPlayer> DewdropIssued;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, IGameCommand> GameCommandIssued;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, IWallet, TPlayer, JB2.Economy.ITreasuryNote> TreasuryNoteAdded;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, IAchievement, TPlayer> AchievementEarned;
        #endregion Events;

        #region Getters
        IEnumerable<IPlayerDewdrop> GetDewdrops();

        IEnumerable<TPlayer> GetPlayers();

        TGameSession FindSessionByPlayer(TPlayer player);

        TGameSession FindSessionByPlayerID(TID playerID);


        IEnumerable<TGameSession> GetSessions();

        IApplication GetApplication();

        #endregion Getters

        #region Methods

        TGameSession StartNewSession(IApplication application);

        void EndSession(TGameSession session);

        void ProcessDewdrops(ProcessDewdrop processDewdrop);
        void ProcessGameCommands(ProcessGameCommand<TPlayer,TID> processCommand);
        void AddPlayer(string sessionID, int seat, TPlayer player);
        void AddPlayer(string sessionID, TPlayer player);

        void AddDewDrop(string dewdropID, TPlayer player, object value);
        void RemovePlayer(string sessionID, int seat);
        void RemovePlayer(string sessionID, TPlayer player);

        void Sync();

        #endregion Methods

    }
}
