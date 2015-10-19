using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.GameObjects;

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

        public JB2.Common.JB2Image RetrieveBingoCardImageByID(string id, string styleCode)
        {
            Common.JB2Image result = null;
            try
            {
                result = _repo.GetBingoCardImage(id, styleCode);
            }
            catch(Exception ex)
            {
                result = null;
                
            }           
            if(result == null)
            {
                //ok the image was not found so lets try to generate one on the fly
                IBingoCard card = _repo.GetBingoCardByID(id);
                if (card == null)
                    return null;

                Common.JB2Image baseImage = _repo.GetBingoCardStyle(styleCode);

                result = BingoHelper.GenerateBingoCardImage(card, baseImage);

                //now that we have the image, let's save it
                _repo.InsertBingoCardImage(card.ID, styleCode, result);
            }

            return result;  
        }

        public JB2.Common.ServiceResult IsValidBingoCard(string id)
        {
            IBingoCard card = null;
            try
            {
                card = _repo.GetBingoCardByID(id);
            }
            catch(Exception ex)
            {
                return false;
            }

            return card != null;
            
        }

        #endregion Bingo Objects

        #region Color Objects

        public IColor RetrieveRandomColor()
        {
            IColor result = new SolidColor(JB2.Common.JB2Color.GetRandom());

            _repo.InsertColor(result);

            return result;
        }

        public IColor RetrieveColorByHex(string hex)
        {
            IColor result;

            try
            {
                result = _repo.GetColorByHex(hex);
            }
            catch(Exception ex)
            {
                result = null;
            }

            if(result == null)
            {
                result = new SolidColor(Common.JB2Color.FromHex(hex));
                _repo.InsertColor(result);
            }

            return result;
        }

        public IColor RetrieveColorByID(string id)
        {
            return _repo.GetColorByID(id);
        }

        #endregion Color Objects
    }
}
