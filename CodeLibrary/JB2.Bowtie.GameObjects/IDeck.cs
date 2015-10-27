using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.GameObjects
{
    public interface IDeck<Tobject> : JB2.Bowtie.IGameObject
        where Tobject : IDeckable<Tobject>
    {
        Tobject[] Stack { get; }

        Tobject[] Discards { get; }

        Tobject Previous { get; }

        int Count{ get; }

        bool isEmpty { get; }

        Tobject TakeTop();

        Tobject Peek();

        JB2.Common.ServiceResult Reshuffle();


        JB2.Common.ServiceResult Reset();

    }
}
