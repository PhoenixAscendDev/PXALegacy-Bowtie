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

        public IGameCommand[] GetByGameID(string gameID)
        {
            var query5 = from i in _dbcontext.jb2bt_Game_Command_Get(null,gameID)
                         select getGameCommand(i);
            return query5.ToArray();
        }
    }
}
