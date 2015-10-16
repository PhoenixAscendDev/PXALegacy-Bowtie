using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.GameObjects;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public interface IGameObject : IBowtieObject, JB2.Common.IIDNamePair<string, string>
    {
        GameObjectType GameObjectType { get; }

        


    }
}
