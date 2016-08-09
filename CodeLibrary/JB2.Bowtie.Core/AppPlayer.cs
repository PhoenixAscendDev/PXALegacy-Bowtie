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
        public ApplicationPlayer(string id, string applicationID) : base()
        {
            _id = id;
            _applicationID = applicationID;
        }



        #endregion Constructors

        #region IApplicationable
        public string GetApplicationID()
        {
            return _applicationID;
        }

        #endregion IApplicationable

        public override string GetIdentityAuthID()
        {
            throw new NotImplementedException();
        }

        public override string DisplayName
        {
            get
            {
                return _metadata["DisplayName"].GetValue().StringValue;
            }

            set
            {
                _metadata["DisplayName"].UpdateValue(value);
            }
        }


        public DateTime DateRegistered
        {
            get
            {
                return _metadata["DateRegistered"].GetValue().DateTimeValue;
            }

            set
            {
                _metadata["DateRegistered"].UpdateValue(value);
            }
        }


        #region Static Methods
        public static ApplicationPlayer FromPlayer(IBowtiePlayer player, IApplication app)
        {
            ApplicationPlayer ap = new ApplicationPlayer(player.GetID(), app.GetID());
            ap.Age = player.Age;
            ap.AuthProvider = player.AuthProvider;
            ap.DisplayName = player.DisplayName;
            ap.Gender = player.Gender;


            return ap;
        }

        #endregion Static Methods








    }
}
