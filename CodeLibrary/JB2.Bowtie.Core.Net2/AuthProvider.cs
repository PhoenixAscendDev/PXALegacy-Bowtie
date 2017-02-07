using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public class AuthProvider : JB2.Bowtie.BowtieObject, IAuthProvider
    {
        public AuthProvider() : base()
        {
            _kind = Enum.BowtieObjectType.bowtie_auth;
        }

        public AuthProvider(string id) : base(id)
        {
            _kind = Enum.BowtieObjectType.bowtie_auth;
        }

        #region Properties

        public string ProfileResourceEndpoint
        {
            get
            {
                return _props.GetProperty<string>("PROFILE_ENDPOINT");
            }

            set
            {
                _props.SetProperty<string>("PROFILE_ENDPOINT",value);
            }
        }

        #endregion Properties


    }
}
