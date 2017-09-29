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

        public ILeaderboardRepository LeaderboardRepository => JB2.Bowtie.Data.Local.LeaderboardRepo.Instance;

        public IInventoryRepository InventoryRepository => JB2.Bowtie.Data.Local.InventoryRepo.Instance;

        public IPlayerRepository PlayerRepository => JB2.Bowtie.Data.Local.PlayerRepo.Instance;

        public IDewdropQueueRepo DewdropQueue => throw new NotImplementedException();

        public ILogRepository LogRepository => JB2.Bowtie.Data.Local.LogRepo.Instance;

        public IDictionaryRepository DictionaryRepository => JB2.Bowtie.Data.Local.DictionaryRepository.Instance;



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
