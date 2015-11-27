using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Core
{
    public interface IBowtiePlayer : JB2.Identity.IPlayer
    {
        string GetIdentityAuthID();
        
    }
}
