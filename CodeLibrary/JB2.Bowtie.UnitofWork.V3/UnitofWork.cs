using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class UnitofWork : IUnitOfWork
    {
        public IApplicationRepository ApplicationRepository => JB2.Bowtie.Data.Local.ApplicationRepo.Instance;

        public IDewdropRepository DewdropRepository => JB2.Bowtie.Data.Local.DewdropRepo.Instance;

        public IAuthorizeRepository AuthorizeRepository => JB2.Bowtie.Data.Local.AuthorizeRepo.Instance;

        public IAchievementRepository AchievementRepository => JB2.Bowtie.Data.Local.AchievementRepo.Instance;

        public IDewdropQueueRepo DewdropQueue => throw new NotImplementedException();



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
