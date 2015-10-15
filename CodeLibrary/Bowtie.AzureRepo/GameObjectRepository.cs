using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.GameObjects;
using JB2.Common;

namespace JB2.Bowtie.Data.Azure
{
    public class GameObjectRepository : JB2.Bowtie.IGameObjectRepository
    {

        #region Bingo 
        public ServiceResult InsertBingoCard(IBingoCard card)
        {
            throw new NotImplementedException();
        }

        public IBingoCard GetBingoCardByID(string id)
        {
            throw new NotImplementedException();
        }
        #endregion 



        public void Delete(IGameObject entity)
        {
            throw new NotImplementedException();
        }

        public IGameObject[] GetAll()
        {
            throw new NotImplementedException();
        }



        public IGameObject GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(IGameObject entity)
        {
            throw new NotImplementedException();
        }


        public IGameObject[] SearchFor()
        {
            throw new NotImplementedException();
        }
    }
}
