using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class SinglePlayerSession : GameSession, IGameSession
    {
        #region Fields
        protected DateTime _endtime;
        protected DateTime _starttime;



        #endregion Fields
        public override DateTime GetEndTime()
        {
            return _endtime;
        }

        public override string GetID()
        {
            return ID;
        }

        public override int GetMaxSeats()
        {
            return 1;
        }

        public override DateTime GetStartTime()
        {
            return _starttime;
        }

        

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void TimeOutStop()
        {
            throw new NotImplementedException();
        }
    }
}
