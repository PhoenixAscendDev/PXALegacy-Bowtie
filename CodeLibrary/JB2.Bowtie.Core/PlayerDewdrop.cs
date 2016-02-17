using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IPlayerDewdrop :  JB2.Identity.IPlayerable
    {
        DateTime GetDewDate();
        string GetDewdropID();
        string GetApplicationID();
        string GetValue();
    }
}
