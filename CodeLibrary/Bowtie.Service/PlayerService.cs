using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class PlayerService : GenericService<IBowtiePlayer, IBowtiePlayerRespository>
    {
        public IBowtiePlayer RetrieveByAuthID(string id,IApplication app)
        {
            var playerid = JB2.Identity.PlayerStore.GetClientPlayerID(id, app.ClientID);

            var appPlayer = JB2.Identity.PlayerStore.GetPlayerByAppPlayerID(playerid, app.GetID());

            IBowtiePlayer result = new JB2.Bowtie.ApplicationPlayer(appPlayer, app.GetID());

            _uofw.PlayerRepository.Insert(result);

            return result;


        }
    }
}
