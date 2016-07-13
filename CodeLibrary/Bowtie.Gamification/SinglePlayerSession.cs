using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{

    public class SinglePlayerSession<TPlayer> : GameSession<TPlayer,string>, IGameSession<TPlayer,string>
        where TPlayer : JB2.Identity.IPlayerable<string>
    {
        #region Constructor

        public SinglePlayerSession() : base()
        {
            ID = "s-" + JB2.Common.NewID.Guid();
        }

        public SinglePlayerSession(TPlayer player) : this()
        {
            _players.Add(1, player);
        }

        #endregion Constructor

        public override string GetID()
        {
            return ID;
        }

        public override int GetMaxSeats()
        {
            return 1;
        }
    }

}
