using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.GameObjects;

namespace JB2.Bowtie
{
    public interface IGameObjectRepository : JB2.Common.IRepository<JB2.Bowtie.IGameObject, string>
    {

        IBingoCard GetBingoCardByID(string id);

        JB2.Common.ServiceResult InsertBingoCard(IBingoCard card);
    }
}
