using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public delegate void ProcessDewdrop(IDewdrop dewdrop, string playerID, object value);
    public delegate void RunGameCommand<TPlayer,TID>(IGameCommand command, IGameSession<TPlayer,TID> session) 
        where TPlayer: JB2.Bowtie.IPlayerable<TID> 
        where TID : IComparable;

    public interface IGameEngine<TGameSession> : IGameEngine<TGameSession,IBowtiePlayer,string>
        where TGameSession: IGameSession<IBowtiePlayer,string>,new()
    {

    }
    public interface IGameEngine<TGameSession,TPlayer,TID>
        where TGameSession : IGameSession<TPlayer,TID>, new()
        where TPlayer :  JB2.Bowtie.IPlayerable<TID>
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
        event Action<IGameEngine<TGameSession, TPlayer, TID>, IPlayerDewdrop, TPlayer> DewdropIssued;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, IGameCommand> GameCommandIssued;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, IWallet, TPlayer, JB2.Economy.ITreasuryNote> TreasuryNoteAdded;

        event Action<IGameEngine<TGameSession, TPlayer, TID>, TPlayer, DateTime> PlayerSignedIn;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, TPlayer, DateTime> PlayerSignedOut;

        event Action<IGameEngine<TGameSession, TPlayer, TID>, TPlayer, JB2.Common.IMetaData> PlayerDataChanged;
        event Action<IGameEngine<TGameSession, TPlayer, TID>, TPlayer, IPlayerInventoryItem> PlayerInventoryChanged;
        #endregion Events;

        #region Getters
        IEnumerable<IPlayerDewdrop> GetDewdrops();

        IEnumerable<TPlayer> GetPlayers();

        int GetDewdropCount(TPlayer player, string Dewdropcode);
        
        TGameSession FindSessionByPlayer(TPlayer player);

        TGameSession FindSessionByPlayerID(TID playerID);
        IEnumerable<TGameSession> GetSessions();

        IApplication GetApplication();

        #endregion Getters

        #region Methods

        TGameSession StartNewSession(IApplication application);

        void EndSession(TGameSession session);

        void ProcessDewdrops(ProcessDewdrop processDewdrop);
        void ProcessGameCommands(RunGameCommand<TPlayer,TID> processCommand);
        void AddPlayer(string sessionID, int seat, TPlayer player);
        void AddPlayer(string sessionID, TPlayer player);

        void AddDewDrop(string dewdropID, TPlayer player, object value);

        void AddGameCommand(string commandCode,string sessionID, TPlayer issuedPlayer, TPlayer affectedPlayer, RunGameCommand<TPlayer,TID> runCommand);
        void RemovePlayer(string sessionID, int seat);
        void RemovePlayer(string sessionID, TPlayer player);

        void SignInPlayer(TPlayer player);

        void SignOutPlayer(TPlayer player);

        void Sync();

        #endregion Methods

    }
}
