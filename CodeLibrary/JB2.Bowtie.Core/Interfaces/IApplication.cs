using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JB2.Bowtie
{
    public interface IApplication : IBowtieObject, JB2.Common.IIDNamePair<string, string>, JB2.Common.IAPIKeySecretPair, JB2.Economy.IRequestor, JB2.Identity.IApplication
    {
        string Secret { get; }
        bool isAuthorized { get; }
        Enum.APIAuthorizeState AuthorizedState { get; }
        string ClientID { get; set; }
        IEnumerable<JB2.Identity.IPlayer> GetAdmins();
    }
}
