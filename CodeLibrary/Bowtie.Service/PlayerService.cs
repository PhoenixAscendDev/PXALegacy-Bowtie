using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class PlayerService : GenericService<IBowtiePlayer, IBowtiePlayerRespository>
    {

        #region Constructors

        public PlayerService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }
        public PlayerService(IUnitOfWork uofw)
        {
            _uofw = uofw;
            _repo = uofw.PlayerRepository;
        }

        public PlayerService(IBowtiePlayerRespository repo)
        {
            _repo = repo;
            _uofw = JB2.Settings.Bowtie.UnitOfWork;
        }

        #endregion Constructors
        public IBowtiePlayer RetrieveByAuthID(string id,IApplication app)
        {
            var playerid = JB2.Identity.PlayerStore.GetClientPlayerID(id, app.ClientID);

            var appPlayer = JB2.Identity.PlayerStore.GetPlayerByAppPlayerID(playerid, app.ClientID);

            return fromIdentity(appPlayer, app);
        }


        public IBowtiePlayer RetrieveByAppPlayerID(string id,IApplication app)
        {
            var appPlayer = JB2.Identity.PlayerStore.GetPlayerByAppPlayerID(id, app.ClientID);

            return fromIdentity(appPlayer, app);
        }

        private IBowtiePlayer fromIdentity(JB2.Identity.IPlayer player, IApplication app)
        {
            IBowtiePlayer result = new JB2.Bowtie.ApplicationPlayer(player, app.GetID());

            WalletService wservice = new WalletService(_uofw);
            var wallet = wservice.RetrieveWalletByPlayer(result, app);

            _uofw.PlayerRepository.Insert(result);

            return result;
        }
    }
}
