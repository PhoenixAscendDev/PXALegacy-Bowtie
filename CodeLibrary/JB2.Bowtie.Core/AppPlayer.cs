using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using JB2.Identity;

namespace JB2.Bowtie
{
    public class ApplicationPlayer : Player, JB2.Identity.IApplicationable<string>
    {
        #region Fields

        protected string _applicationID;


        #endregion Fields


        #region Constructors
        public ApplicationPlayer(JB2.Identity.IPlayer authPlayer, string applicationID)
        {
            _player = authPlayer;
            _id = authPlayer.GetDefaultProfile().ID;
            _applicationID = applicationID;
        }

        public override string DisplayName
        {
            get
            {
                return _player.DisplayName;
            }

            set
            {
                _player.DisplayName = value;
            }
        }

        #endregion Constructors

        #region IApplicationable
        public string GetApplicationID()
        {
            return _applicationID;
        }



        #endregion IApplicationable

        #region IBowtiePlayer

        public override int GetBitScore()
        {
            throw new NotImplementedException();
        }

        public override string GetFamilyID()
        {
            return _player.GetFamilyID();
        }

        public override string GetIdentityAuthID()
        {
            throw new NotImplementedException();
        }

        public override string GetjBeanAccountNumber()
        {
            throw new NotImplementedException();
        }

        public override string GetMasterEmail()
        {
            throw new NotImplementedException();
        }

        public override string GetMasterUsername()
        {
            throw new NotImplementedException();
        }

        public override Name GetName()
        {
            return _player.Name;
        }

        public override IEnumerable<PlayerProfilePacket> GetProfiles()
        {
            var packet = this._player.GetDefaultProfile();
            return new PlayerProfilePacket[1] { packet };
        }


        #endregion IBowtiePlayer



    }
}
