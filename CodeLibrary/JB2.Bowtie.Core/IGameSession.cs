using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IGameSession:  IGameSession<IBowtiePlayer,string>
    {

    }
    
    public interface IGamSession<TPlayer> : IGameSession<TPlayer,string>
        where TPlayer : JB2.Bowtie.IPlayerable<string>
    {
       
    }
    public interface IGameSession<TPlayer,TID>
        where TPlayer :  JB2.Bowtie.IPlayerable<TID>
        where TID : IComparable
    {
        #region Getters

        TID GetSessionID();
        DateTime GetStartTime();
        DateTime GetEndTime();
        IDictionary<int, TPlayer> GetPlayers();
        int GetMaxSeats();
        #endregion Getters

        void AddPlayer(TPlayer player);
        void AddPlayer(TPlayer player, int seat);

        void RemovePlayer(TPlayer player);
        void RemovePlayerBySeat(int seat);

        void Start();
        void ManuallyStop();
        void TimeOutStop();
        IEnumerable<IGameCommand> GetGameCommands();

        void AddGameCommand(IGameCommand command);




        event Action<IGameSession<TPlayer, TID>, DateTime> Started;
        event Action<IGameSession<TPlayer, TID>, DateTime> ManuallyStopped;
        event Action<IGameSession<TPlayer, TID>, DateTime> TimedOut;
        event Action<IGameSession<TPlayer, TID>, TPlayer, int> PlayerAdded;
        event Action<IGameSession<TPlayer, TID>, TPlayer, int> PlayerRemoved;
        event Action<IGameSession<TPlayer, TID>, int> NoVacancy;
        event Action<IGameSession<TPlayer, TID>, int> Vacancy;


    }
}
