using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class BowtiePlayer : Player, IBowtiePlayer
    {


        #region Constructors
        public BowtiePlayer() : base()
        {

        }


        #endregion Constructors
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

        public override string GetIdentityAuthID()
        {
            if (_metadata["AuthID"] != null)
                return _metadata["AuthID"].GetValue().StringValue;
            else
                return string.Empty;
        }
    }
}
