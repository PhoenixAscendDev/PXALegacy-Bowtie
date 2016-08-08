using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public abstract class GameSession<TPlayer,TID> : JB2.Common.IDValue<TID>,IGameSession<TPlayer,TID>
        where TPlayer : JB2.Bowtie.IPlayerable<TID>
        where TID : IComparable
    {
        #region Fields
        protected IDictionary<int, TPlayer> _players;
        protected DateTime _startTime;
        protected DateTime _endTime;
        protected JB2.Common.BaseCollection<IGameCommand> _commands;        

        #endregion Fields

        #region Constructors
        public GameSession(): base()
        {
            _players = new Dictionary<int, TPlayer>();
        }

        #endregion Constructors


        public event Action<IGameSession<TPlayer,TID>, DateTime> ManuallyStopped;
        public event Action<IGameSession<TPlayer, TID>, int> NoVacancy;
        public event Action<IGameSession<TPlayer, TID>, TPlayer, int> PlayerAdded;
        public event Action<IGameSession<TPlayer, TID>, TPlayer, int> PlayerRemoved;
        public event Action<IGameSession<TPlayer, TID>, DateTime> Started;
        public event Action<IGameSession<TPlayer, TID>, DateTime> TimedOut;
        public event Action<IGameSession<TPlayer, TID>, int> Vacancy;
        public event Action<IGameSession<TPlayer, TID>, IGameCommand> CommandAdded;

        public virtual void AddPlayer(TPlayer player)
        {
            int newSeat = _players.Keys.Max() + 1;
            AddPlayer(player, newSeat);
        }

        public void AddPlayer(TPlayer player, int seat)
        {
            if (_players.Count() < GetMaxSeats())
            {               
                _players.Add(seat, player);
                if (PlayerAdded != null)
                    PlayerAdded(this, player, seat);
            }

            if (_players.Count() >= GetMaxSeats())
            {
                if (NoVacancy != null)
                    NoVacancy(this, _players.Count());
            }
            
        }

        public virtual DateTime GetEndTime()
        {
            return _endTime;
        }


        public abstract int GetMaxSeats();

        public virtual IDictionary<int, TPlayer> GetPlayers()
        {
            return _players;
        }

        public TID GetSessionID()
        {
            return ID;
        }

        public virtual DateTime GetStartTime()
        {
            return _startTime;
        }

        public virtual IEnumerable<IGameCommand> GetGameCommands()
        {
            return _commands;
        }

        public virtual void AddGameCommand(IGameCommand command)
        {
            _commands.Add(command);
        }

        public virtual void ManuallyStop()
        {
            _endTime = DateTime.Now;
            if (ManuallyStopped != null)
                ManuallyStopped(this, _endTime);
        }
       
        public virtual void RemovePlayer(TPlayer player)
        {
            throw new NotImplementedException();
        }

        public virtual void RemovePlayerBySeat(int seat)
        {
            var player = _players[seat];

            if (player != null)
            {
                _players.Remove(seat);
                if (PlayerRemoved != null)
                    PlayerRemoved(this, player, seat);
            }             
        }

        public virtual void Start()
        {
            _startTime = DateTime.Now;
            if (Started != null)
                Started(this, _startTime);  
        }
              
        public virtual void TimeOutStop()
        {
            _endTime = DateTime.Now;
            if (TimedOut != null)
                TimedOut(this, _endTime);
        }
       
    }
}
