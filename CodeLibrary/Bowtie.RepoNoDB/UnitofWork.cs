using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.NoDB
{
    public class UnitofWork : JB2.Bowtie.IUnitOfWork
    {


        public IApplicationRepository ApplicationRepository
        {
            get 
            {
                return (IApplicationRepository)GetRepository(Enum.RepositoryType.Application);

            }
        }

        public IAchievementRepository AchievementRepository
        {
            get
            {
                return (IAchievementRepository)GetRepository(Enum.RepositoryType.Achievement);
            
            }
        }

        public IGameCommandRepository GameCommandRepository
        {
            get
            {
                return (IGameCommandRepository)GetRepository(Enum.RepositoryType.GameCommand);

            }
        }



        public object GetRepository(Enum.RepositoryType respository)
        {
            switch(respository)
            {
                case Enum.RepositoryType.Achievement:
                    return new AchievementRepository();
                case Enum.RepositoryType.Application:
                    return new ApplicationRepository();
                case Enum.RepositoryType.GameCommand:
                    return new GameCommandRepository();
            }

            return null;
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
