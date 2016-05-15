using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IBowtiePlayer : JB2.Identity.IPlayer, IBowtieObject
    {
        string GetIdentityAuthID();
        JB2.Bowtie.IWallet GetWallet();   
    }
}
