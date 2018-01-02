using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IAchievementRepository :  JB2.Common.IRepository<JB2.Bowtie.IAchievement, string>
    {
        IEnumerable<IAchievement> GetByApplicationID(string applicationID);


        IEnumerable<AchievementStepRule> GetStepsByAchievementID(string achievementID);

        void Insert(AchievementStepRule rule);

        void Delete(AchievementStepRule rule);

        JB2.Bowtie.AchievementData GetDataByPlayer(string applicationID, string playerID);
        JB2.Common.ServiceResult InsertAchievementData(string applicationID, string playerID, AchievementData data);

        IEnumerable<IAchievementEntry> GetEntriesByPlayer(string applicationID, string playerID);



    }
}
