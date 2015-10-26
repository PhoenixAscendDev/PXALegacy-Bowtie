using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

using JB2.Bowtie.GameObjects;

namespace JB2.Bowtie.GameObjects.Bingo
{
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
            _startqueue = balls.ToList();
            _queue = _startqueue; // new List<BingoBall<Tnum>>(balls.Length);
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
                throw new NotImplementedException();
            }
        }

        public BingoBall<Tnum>[] PreviousCalled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public BingoBall<Tnum> PreviousBall
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool isDone
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public GameObjectType GameObjectType
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        #endregion Properties

        #region Methods

        public BingoBall<Tnum> CallNext()
        {
            throw new NotImplementedException();
        }

        public ServiceResult ResetQueue()
        {
            throw new NotImplementedException();
        }

        #endregion Methods


    }
}
