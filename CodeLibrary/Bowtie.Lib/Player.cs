using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;


namespace JB2.Bowtie
{
    public class Player
    {
        #region Fields
        private JB2.Identity.IPlayer _player;
                         
        #endregion Fields

        public Player(JB2.Identity.IPlayer identityPlayer)
        {
            _player = identityPlayer;
        }
    }
}
