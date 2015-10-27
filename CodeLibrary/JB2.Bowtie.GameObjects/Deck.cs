using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie.GameObjects
{
    public class Deck<Titem> : BowtieObject, IDeck<Titem>
    {

        #region Fields

        protected List<Titem> _startqueue;

        protected List<Titem> _queue;

        protected List<Titem> _previous;

        #endregion Fields

        #region Constructors

        public Deck(string id, Titem[] items) : base(BowtieObjectType.bowtie_gameobject,id)
        {
            // due to List performance faster to add/remove at the bottom so lets reverse it so we pop the bottom
            _startqueue = items.Reverse().ToList();
            _queue = _startqueue;
            _previous = new List<Titem>(items.Length);
        }

        private Deck(Titem[] items) : this(null,items)
        {

        }

        #endregion Constructors

        #region Properties

        public Titem[] Stack
        {
            get
            {
                return (Titem[])_queue.ToArray().Reverse();
            }
        }

        public Titem[] Discards
        {
            get
            {
                return _previous.ToArray();
            }
        }

        public Titem Previous
        {
            get
            {
                return _previous.Last();
            }
        }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }


        public bool isEmpty
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

        public Titem TakeTop()
        {
            Titem item = _queue.Last();

            //now remove that ball from the queue and add it to the previous
            _previous.Add(item);
            _queue.RemoveAt(_queue.Count - 1);

            return item;
        }

        public Titem Peek()
        {
            return _queue.Last();
        }

        public ServiceResult ResetQueue()
        {
            _queue = _startqueue;
            _previous = new List<Titem>();

            return true;
        }

    }
}
