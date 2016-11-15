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
        public IBowtiePlayer RetrieveByAuth(AuthInfo info)
        {
            return _repo.GetPlayerByAuth(info.UserID, info.ProviderID);
        }

        //public IBowtiePlayer RetrieveByAppPlayerID(string id,IApplication app)
        //{
        //    var appPlayer = JB2.Identity.PlayerStore.GetPlayerByAppPlayerID(id, app.ClientID);

        //    return fromIdentity(appPlayer, app);
        //}
        public ApplicationPlayer RegisterPlayer(JB2.Bowtie.IBowtiePlayer player, IApplication app, string authprovider)
        {
            // create player
            ApplicationPlayer result = ApplicationPlayer.FromPlayer(player, app);
            AuthInfo authInfo = new AuthInfo();
            authInfo.ProviderID = authprovider;
            result.AuthInfo = authInfo;
            result.DateRegistered = DateTime.Now;
            _repo.Insert(result);

            // create wallet
            WalletService wservice = new WalletService(this._uofw);
            IWallet wallet = wservice.RetrieveWalletByPlayer(player, app);

           
            // set the achievements
            AchievementService aservice = new AchievementService(this._uofw);
            aservice.InitilizePlayerAchievements(player, app);

            // do the dew
            DewdropService dservice = new DewdropService(this._uofw);
            var dew = dservice.RetrieveById("dew_4CBg");
            var pdew = dservice.GenerateNewPlayerDewdrop(player, dew, "REGISTER");


            if(dservice.Validate(player, dew))
            {
                dservice.Save(pdew);
            }
            return result;
        }

    }
}
