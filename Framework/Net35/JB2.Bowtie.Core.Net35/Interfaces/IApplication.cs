using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IApplication : IBowtieObject, JB2.Common.IAPIKeySecretPair, JB2.Common.IIDNamePair<string, string>, IApplicationable<string>
    {
        string Website { get; set; }

        bool IsAuthorized { get; }

        Enum.APIAuthorizeState AuthorizedState { get; }

        JB2.Common.IBusiness Company { get; set; }


    }
}
