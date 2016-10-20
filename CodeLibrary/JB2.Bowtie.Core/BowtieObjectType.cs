using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Attributes;

namespace JB2.Bowtie.Enum
{
    public enum BowtieObjectType
    {
        [TokenName("Bowtie>*<???")]
        unknown,
        [TokenName("Bowtie>*<Command")]
        bowtie_command,
        [TokenName("Bowtie>*<Player")]
        bowtie_player,
        [TokenName("Bowtie>*<Application")]
        bowtie_application,
        [TokenName("Bowtie>*<Leaderboard")]
        bowtie_leaderboard,
        [TokenName("Bowtie>*<Client")]
        bowtie_client,
        [TokenName("Bowtie>*<PlayerAchievement")]
        bowtie_playerachievement,
        [TokenName("Bowtie>*<Achievement")]
        bowtie_achievement,
        [TokenName("Bowtie>*<MasterLeaderboard")]
        bowtie_masterLeaderboard,
        [TokenName("Bowtie>*<GameObject")]
        bowtie_gameobject,
        [TokenName("Bowtie>*<Module")]
        bowtie_module
    }
}
