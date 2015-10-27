using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

using JB2.Bowtie.GameObjects;
using JB2.Common.Extensions;

namespace JB2.Bowtie.GameObjects.Bingo
{

    public class BingoBallQueue :  BingoBallQueue<byte>
    {
        public BingoBallQueue(string id, BingoBall<byte>[] balls) : base(id,balls)
        {

        }

    }


    public class BingoBallQueue<Tnum> : BowtieObject, IBingoBallQueue<Tnum>
    {

        #region Fields

        List<BingoBall<Tnum>> _startqueue;

        List<BingoBall<Tnum>> _queue;

        List<BingoBall<Tnum>> _previous;

        #endregion Fields

        #region Constructors


        public BingoBallQueue(string id, BingoBall<Tnum>[] balls) : base(BowtieObjectType.bowtie_gameobject,id)
        {
            // due to List performance faster to add/remove at the bottom so lets reverse it so we pop the bottom
            _startqueue = balls.Reverse().ToList();
            _queue = _startqueue; 
            _previous = new List<BingoBall<Tnum>>(balls.Length);
        }

        private BingoBallQueue(BingoBall<Tnum>[] balls) : base(BowtieObjectType.bowtie_gameobject,null)
        {

        }

        

        #endregion Constructors

        #region Properties

        public BingoBall<Tnum>[] Queue
        {
            get
            {
                return (BingoBall<Tnum>[])_queue.ToArray().Reverse();
            }
        }

        public BingoBall<Tnum>[] PreviousCalled
        {
            get
            {
                return _previous.ToArray();
            }
        }

        public BingoBall<Tnum> PreviousBall
        {
            get
            {
                return _previous.Last();
            }
        }

        public bool isDone
        {
            get
            {
                return _queue.Count == 0;
            }
        }

        public GameObjectType GameObjectType
        {
            get
            {
                return GameObjectType.BingoBallQueue;
            }
        }
        #endregion Properties

        #region Methods

        public BingoBall<Tnum> CallNext()
        {
            BingoBall<Tnum> ball = _queue.Last();

            //now remove that ball from the queue and add it to the previous
            _previous.Add(ball);
            _queue.RemoveAt(_queue.Count - 1);

            return ball;
        }

        public JB2.Common.ServiceResult MoveBallUp(BingoBall<Tnum> ball, int positions)
        {
            JB2.Common.ServiceResult result = true;

            int currentPosition = _queue.FindIndex(x => x == ball);

            if (currentPosition >= 2)
            {
                _queue.Move(positions, Common.Enum.ElevatorDirection.Up);
                int newPosition = _queue.FindIndex(x => x == ball);
                if (newPosition != currentPosition)
                    result.Validation.Add(new Validation("Position", newPosition.ToString()) { IsValid = true});
                else
                    result.Validation.Add(new Validation("InvalidPosition", "Bingo Ball is already in the top show") { IsValid = false });
            }

            return false;
        }

        public ServiceResult ResetQueue()
        {
            _queue = _startqueue;
            _previous = new List<BingoBall<Tnum>>();

            return true;
        }

        #endregion Methods

    }
}
