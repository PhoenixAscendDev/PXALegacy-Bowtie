using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects.Bingo
{
    public interface IBingoBallQueue<Tnum> : JB2.Bowtie.IGameObject
    {
         BingoBall<Tnum>[]  Queue { get; }
       
         BingoBall<Tnum>[]  PreviousCalled { get; }

        BingoBall<Tnum> PreviousBall { get; }

        bool isDone { get; }

        BingoBall<Tnum> CallNext();

        JB2.Common.ServiceResult ResetQueue();
        
    }
}
