using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    [GraphStory("s280D5DAD")]
    public interface IPlayerDewdrop :  JB2.Bowtie.IPlayerable,JB2.Common.IIDNamePair<string,string>
    {
        DateTime GetDewDate();
        string GetDewdropID();
        string GetApplicationID();
        string GetValue();
    }
}
