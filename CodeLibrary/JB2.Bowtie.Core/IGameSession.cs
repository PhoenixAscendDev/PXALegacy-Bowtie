using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IGameSession
    {
        #region Getters

        string GetSessionID();
        DateTime GetStartTime();
        DateTime GetEndTime();
        IDictionary<int, IBowtiePlayer> GetPlayers();
        int GetMaxSeats();
        #endregion Getters

        void AddPlayer(IBowtiePlayer player);
        void AddPlayer(IBowtiePlayer player, int seat);

        void RemovePlayer(IBowtiePlayer player);
        void RemovePlayerBySeat(int seat);

        void Start();
        void ManuallyStop();
        void TimeOutStop();


        event Action<IGameSession,DateTime> Started;
        event Action<IGameSession,DateTime> ManuallyStopped;
        event Action<IGameSession,DateTime> TimedOut;
        event Action<IGameSession, IBowtiePlayer, int> PlayerAdded;
        event Action<IGameSession, IBowtiePlayer, int> PlayerRemoved;
        event Action<IGameSession, int> NoVacancy;
        event Action<IGameSession, int> Vacancy;


    }
}
