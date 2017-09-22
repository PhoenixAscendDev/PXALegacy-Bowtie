using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IApplication : IBowtieObject, JB2.Common.IAPIKeySecretPair, JB2.Common.IIDNamePair<string, string>
    {
        string Website { get; set; }

        bool IsAuthorized { get; }

        Enum.APIAuthorizeState AuthorizedState { get; }

        JB2.Common.IBusiness Company { get; set; }


    }
}
