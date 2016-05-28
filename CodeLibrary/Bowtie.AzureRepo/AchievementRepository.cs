using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Bowtie.Data.Azure
{
    public class AchievementRepository : BowtieRepository<IAchievement>, IAchievementRepository
    {


        public IAchievement[] GetAchievementsByApplication(string appID)
        {
            throw new NotImplementedException();
        }

        public override IAchievement[] GetAll(bool useCache = true)
        {
            throw new NotImplementedException();
        }

        public override IAchievement GetById(string id, bool useCache = true)
        {
            throw new NotImplementedException();
        }

        public IPlayerAchievement[] GetPlayerAchievements(string playerID, string appID)
        {
            throw new NotImplementedException();
        }

        public bool SavePlayerAchievements(IPlayerAchievement playerAchievement)
        {
            throw new NotImplementedException();
        }

        public override IAchievement[] SearchFor(bool useCache = true)
        {
            throw new NotImplementedException();
        }

        public override IAchievement[] SearchFor(string filter, bool useCache = true)
        {
            throw new NotImplementedException();
        }

        protected override DynamicTableEntity convertToEntity(IAchievement o)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<IAchievement> convertToObject(IEnumerable<DynamicTableEntity> list)
        {
            throw new NotImplementedException();
        }

        protected override IAchievement convertToObject(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void deleteAll(DynamicTableEntity e)
        {
            throw new NotImplementedException();
        }

        protected override void saveEntity(DynamicTableEntity e, bool replace)
        {
            throw new NotImplementedException();
        }
    }
}
