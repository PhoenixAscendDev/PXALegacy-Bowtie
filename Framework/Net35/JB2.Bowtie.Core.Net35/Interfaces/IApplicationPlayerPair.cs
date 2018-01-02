using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IApplicationPlayerPair: IApplicationPlayerPair<string,string>
    {

    }

    public interface IApplicationPlayerPair<Tplayer, Tapplication> :  IPlayerable<Tplayer>, IApplicationable<Tapplication>
    {
        Tplayer PlayerID { get; set; }
        Tapplication ApplicationID { get; set; }
        string GetKey();
    }
}
