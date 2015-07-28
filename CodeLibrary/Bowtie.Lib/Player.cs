using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Player : BowtieObject, IPlayer
    {
        private string _displayName;
        private Common.Name _nameinfo;

        public Player() : this(null)
        {

        }
        public Player(string id) : base(Enum.BowtieObjectType.bowtie_player, id)
        {
            _displayName = "newguy";
            _nameinfo = new Common.Name();
        }
        
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }

        public Common.Name NameInfo
        {
            get
            {
                return _nameinfo;
            }
            set
            {
                _nameinfo = value;
            }
        }
    }
}
