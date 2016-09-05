using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IBowtieGroup<Tplayer,Tkey,Tsearch> : JB2.Common.IIDNamePair<string,string>,IBowtieObject, JB2.Common.IObjectCollection<Tplayer>, IApplicationable<Tkey>
        where Tplayer: IPlayerable<Tkey>
        where Tkey : IComparable
    {

        IEnumerable<Tplayer> Search(Tsearch search);

        bool DoesExist(Tplayer player);

        IList<Tplayer> ToList();
    }
}
