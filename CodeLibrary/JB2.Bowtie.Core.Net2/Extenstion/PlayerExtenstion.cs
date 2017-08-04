using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public static class PlayerExtension
    {

        public static JB2.Bowtie.ProfileResourcePacket ToProfilePacket(this JB2.Bowtie.IBowtiePlayer player)
        {
            JB2.Bowtie.ProfileResourcePacket packet = new ProfileResourcePacket();

            packet.ProviderId = player.GetAuthInfo().ProviderID;
            packet.UserId = player.GetAuthInfo().UserID;
           
            packet.FirstName = ((Player)player).Name.First;
            packet.DisplayName = player.DisplayName;
            packet.LastName = ((Player)player).Name.Last;

            packet.ProfilePicURL = string.Empty;
            packet.AgeRangeMax = 0;
            packet.AgeRangeMin = 100;
            packet.BirthDay = 1;
            packet.BirthMonth = 1;

            return packet;
        }


    }
}
