using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class GameObjectService : GenericService<IGameObject, IGameObjectRepository>
    {

        public GameObjectService()
        {

        }

        public GameObjectService(IUnitOfWork unitOfWork, IGameObjectRepository repo) : base(repo)
        {
            _uofw = unitOfWork;
        }


        #region Bingo Objects

        public JB2.Common.JB2Image RetrieveBingoCardImageByID(string id, string backgroundCode)
        {

            return new Common.JB2Image();

        }

        #endregion Bingo Objects
    }
}
