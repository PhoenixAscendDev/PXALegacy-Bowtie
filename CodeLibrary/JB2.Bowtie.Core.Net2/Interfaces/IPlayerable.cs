using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{ 
    public interface IPlayerable : IPlayerable<string>
    {

    }
    public interface IPlayerable<T>
    {
        T GetPlayerID();
    }
}
