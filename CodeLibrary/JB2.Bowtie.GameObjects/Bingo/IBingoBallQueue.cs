using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{

    public interface IBingoBallQueue : IBingoBallQueue<byte>
    {

    }

    public interface IBingoBallQueue<Tnum> : JB2.Bowtie.IGameObject
    {
         BingoBall<Tnum>[]  Queue { get; }
       
         BingoBall<Tnum>[]  PreviousCalled { get; }

        BingoBall<Tnum> PreviousBall { get; }

        bool isEmpty { get; }

        BingoBall<Tnum> CallNext();

        BingoBall<Tnum> Peek();

        JB2.Common.ServiceResult ResetQueue();
        
    }
}
