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
            //var playerid = JB2.Identity.PlayerStore.GetClientPlayerID(id, app.ClientID);

            //var appPlayer = JB2.Identity.PlayerStore.GetPlayerByAppPlayerID(playerid, app.ClientID);

            //return fromIdentity(appPlayer, app);
            throw new NotImplementedException();
        }

        //public IBowtiePlayer RetrieveByAppPlayerID(string id,IApplication app)
        //{
        //    var appPlayer = JB2.Identity.PlayerStore.GetPlayerByAppPlayerID(id, app.ClientID);

        //    return fromIdentity(appPlayer, app);
        //}


        public void RegisterPlayer(JB2.Bowtie.IBowtiePlayer player, IApplication app, string authprovider)
        {
            // create player
            ApplicationPlayer result = ApplicationPlayer.FromPlayer(player, app);
            result.AuthProvider = authprovider;
            result.DateRegistered = DateTime.Now;
            _repo.Insert(result);

            // create wallet
            WalletService wservice = new WalletService(this._uofw);
            IWallet wallet = wservice.GenerateWalletForPlayer(player, app);

            // set the achievements
            AchievementService aservice = new AchievementService(this._uofw);
            aservice.InitilizePlayerAchievements(player, app);



        }

        //private IBowtiePlayer fromIdentity(JB2.Identity.IPlayer player, IApplication app)
        //{
        //    IBowtiePlayer result = new JB2.Bowtie.ApplicationPlayer(player, app.GetID());

        //    WalletService wservice = new WalletService(_uofw);
        //    var wallet = wservice.RetrieveWalletByPlayer(result, app);

        //    _uofw.PlayerRepository.Insert(result);

        //    return result;
        //}

    }
}
