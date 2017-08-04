using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IGameCommandRepository : JB2.Common.IRepository<JB2.Bowtie.IGameCommand, string>
    {
        IGameCommand[] GetByGameID(string gameID);
    }
}
