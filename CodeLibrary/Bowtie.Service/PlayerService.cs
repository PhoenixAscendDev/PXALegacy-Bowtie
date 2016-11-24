using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

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

        public ApplicationPlayer Signin(IApplicationable<string> application, AuthInfo authInfo)
        {
            //test to make sure Application exists and is valid
            ApplicationService appService = new ApplicationService(_uofw);
            AuthProviderService authService = new AuthProviderService(_uofw);
            try
            {
                ApplicationPlayer result = null;
                ServiceResult isAuthorized = appService.isAuthorized(application.GetApplicationID());
               
                if (!isAuthorized)
                    throw new Exception("Application Not Authorized during Signin");

                IBowtiePlayer player = RetrieveByAuth(authInfo);
                var app = appService.RetrieveById(application.GetApplicationID());

                var profilePacket = authService.GetProfilePacket(authInfo);
                //player null means never used this Auth before, so get profile info and create user
                if (player == null)
                {
                    player = CreateNewPlayer(profilePacket);
                    ((Player)player).AuthInfo = authInfo;
                    _repo.InsertAuthInfo(player.GetID(), authInfo);                  
                }

               result = _repo.GetAppPlayerByID(player.GetID(), application.GetApplicationID());

               //player exists but never was registered for app;
               if(result == null)
                    result = RegisterPlayer(player, app, authInfo.ProviderID);
               
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return null;
            }


        }


        public IBowtiePlayer CreateNewPlayer()
        {
            return CreateNewPlayer(new ProfileResourcePacket());
        }

        public IBowtiePlayer CreateNewPlayer(ProfileResourcePacket packet)
        {
            string id = JB2.Helper.Bowtie.GenerateID<BowtiePlayer>();

            var modules = _uofw.ModuleRepository.GetAll();

            BowtiePlayer newPlayer = new BowtiePlayer(id, modules);
            newPlayer.Name.First = packet.FirstName;
            newPlayer.Name.Middle = string.Empty;
            newPlayer.Name.Last = packet.LastName;
            newPlayer.AgeRange = new SmallNumberRange() { Max = packet.AgeRangeMax, Min = packet.AgeRangeMin };
            newPlayer.BirthDayOfMonth = packet.BirthDay;
            newPlayer.BirthMonth = packet.BirthMonth;
            newPlayer.DisplayName = packet.DisplayName;


            _repo.Insert(newPlayer);
            return newPlayer;
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
