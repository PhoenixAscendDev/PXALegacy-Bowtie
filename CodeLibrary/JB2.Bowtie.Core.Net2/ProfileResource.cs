using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public struct ProfileResourcePacket
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public byte AgeRangeMin { get; set; }

        public byte AgeRangeMax { get; set; }

        public string Email { get; set; }

        public string ProfilePicURL { get; set; }

        public short BirthMonth { get; set; }

        public short BirthDay { get; set; }

        public string UserId { get; set; }

        public string ProviderId { get; set; }







    }
}
