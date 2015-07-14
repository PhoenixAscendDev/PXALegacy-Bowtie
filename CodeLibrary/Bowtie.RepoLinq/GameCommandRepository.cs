using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Linq
{
    public class GameCommandRepository: LinqRepository<IGameCommand>, IGameCommandRepository
    {
        public GameCommandRepository(BowtieDataContext context)  : base(context, Enum.BowtieObjectType.bowtie_command)
        {
           

        }

        public IGameCommand[] GetGameCommandsByGameID(string gameID)
        {
            throw new NotImplementedException();
        }
    }
}
