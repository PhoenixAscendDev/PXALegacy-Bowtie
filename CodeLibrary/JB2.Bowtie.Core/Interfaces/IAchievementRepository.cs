using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IAchievementRepository : JB2.Common.IRepository<JB2.Bowtie.IAchievement, string>
    {
        IPlayerAchievement[] GetPlayerAchievements(string playerID, string appID);

        bool SavePlayerAchievements(IPlayerAchievement playerAchievement);

        IAchievement[] GetAchievementsByApplication(string appID);
    }
}
