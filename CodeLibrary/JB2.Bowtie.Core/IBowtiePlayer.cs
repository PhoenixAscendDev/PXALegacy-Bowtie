using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IBowtiePlayer : JB2.Common.IPerson<string>, IPlayerable<string>,IPlayerable, IBowtieObject
    {
        string GetIdentityAuthID();
        JB2.Bowtie.IWallet GetWallet();  
        int Age { get; set; }
        string Gender { get; set; }
        string AuthProvider { get; set; }     
        IDictionary<string,int> DewdropCounts { get; set; }
        void AddDewDrop(IDewdrop dewdrop);
    }
}
