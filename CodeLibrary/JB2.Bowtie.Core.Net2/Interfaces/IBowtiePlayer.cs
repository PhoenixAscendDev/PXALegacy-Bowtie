using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IBowtiePlayer : JB2.Common.IPerson<string>, IPlayerable<string>, IPlayerable, IBowtieObject
    {
        AuthInfo GetAuthInfo();
        JB2.Bowtie.IWallet GetWallet();

        Backpack GetBackpack();

        BowtieMetadata GetModuleData(string moduleid);
        IEnumerable<IPlayerInventoryItem> GetModuleInventory(string moduleid);


        JB2.Common.IRange<byte> AgeRange { get; set; }
        short BirthMonth { get; set; }
        short BirthDayOfMonth { get; set; }


        //int Age { get; set; }
        //string Gender { get; set; }
        IDictionary<string,int> DewdropCounts { get; set; }
        void AddDewDrop(IDewdrop dewdrop);
    }
}
