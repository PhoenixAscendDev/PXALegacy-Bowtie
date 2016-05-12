using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.NoDB
{
    public class GameCommandRepository :  JB2.Bowtie.IGameCommandRepository
    {
        public IGameCommand[] GetByGameID(string gameID)
        {
            throw new NotImplementedException();
        }

        public void Delete(IGameCommand entity)
        {
            throw new NotImplementedException();
        }

        public IGameCommand[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IGameCommand GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(IGameCommand entity)
        {
            throw new NotImplementedException();
        }

        public IGameCommand[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IGameCommand[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
