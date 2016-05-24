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
        event Action<IGameEngine> Started;
        event Action<IGameEngine> Stopped;
        event Action<IAchievement, IBowtiePlayer> AchievementUnlocked;
        event Action<IAchievement, IBowtiePlayer> AchievementUpdated;
        event Action<IGameEngine, JB2.Economy.ITreasuryNote, IBowtiePlayer> jBeanAwarded;  
        event Action<IBowtiePlayer, int> PlayerAdded;
        event Action<IBowtiePlayer, int> PlayerDropped;
        event Action<IPlayerDewdrop> DewdropRegistered;
        event Action<IGameCommand> GameCommandIssued;
        #endregion Events;

        #region Getters
        IEnumerable<IPlayerDewdrop> GetDewdrops();

        IDictionary<int, IBowtiePlayer> GetPlayers();

        #endregion Getters

        #region Methods

        void StartNewGame(IApplication application);

        void EndGame();

        void RegisterDewdrop(string dewdropID, string playerID, object value);

        void ProcessGameCommand(IGameCommand command);
        void AddPlayer(int seat, IBowtiePlayer player);
        void AddPlayer(IBowtiePlayer player);
        void RemovePlayer(int seat);
        void RemovePlayer(IBowtiePlayer player);

        #endregion Methods

    }
}
