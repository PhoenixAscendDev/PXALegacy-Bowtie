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

        public GameObjects.IBingoCard RetrieveRandomBingoCard()
        {
            GameObjects.IBingoCard card = GameObjects.BingoHelper.GenerateNewBingoCard();

            //this needs to be improved
            _repo.InsertBingoCard(card);

            return card;
        }

        public GameObjects.IBingoCard RetrieveBingoCardByID(string id)
        {
            return _repo.GetBingoCardByID(id);
        }

        public Common.ServiceResult SaveBingoCard(GameObjects.IBingoCard card)
        {
            return _repo.InsertBingoCard(card);

        }

        public JB2.Common.JB2Image RetrieveBingoCardImageByID(string id, string backgroundCode)
        {

            return new Common.JB2Image();

        }

        #endregion Bingo Objects
    }
}
