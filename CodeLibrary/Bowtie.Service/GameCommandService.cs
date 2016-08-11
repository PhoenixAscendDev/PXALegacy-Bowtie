using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class GameCommandService : GenericService<IGameCommand,IGameCommandRepository>
    {
        public GameCommandService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {
           
        }

        public GameCommandService(IUnitOfWork unitOfWork) : this(unitOfWork.GameCommandRepository)
        {
            _uofw = unitOfWork;
        }

        public GameCommand New(string code = "", string applicationID = "", string gameID = "")
        {

            GameCommand gc = new GameCommand();
            gc.CommandCode = code;
            gc.AppID = applicationID;
            gc.GameID = gameID;

            return gc;
        }

        public GameCommandService(IGameCommandRepository repo)
            : base(repo)
        {

        }

        public IGameCommand[] RetrieveByGameID(string gameID)
        {
            return _uofw.GameCommandRepository.GetByGameID(gameID);
        }

    }
}
