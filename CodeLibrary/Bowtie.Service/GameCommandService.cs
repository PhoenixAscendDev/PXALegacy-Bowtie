using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class GameCommandService : GenericService<IGameCommand,IGameCommandRepository>
    {
        public GameCommandService()
        {
           
        }

        public GameCommandService(IUnitOfWork unitOfWork) : this(unitOfWork.GameCommandRepository)
        {
            _uofw = unitOfWork;
        }

        public GameCommandService(IGameCommandRepository repo)
            : base(repo)
        {

        }

    }
}
