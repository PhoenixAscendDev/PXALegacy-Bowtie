using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JB2.Bowtie
{
    public interface IApplication : IBowtieObject, JB2.Common.IIDNamePair<string, string>, JB2.Common.IAPIKeySecretPair, JB2.Economy.IRequestor, JB2.Identity.IApplication
    {
        string Secret { get; set; }
        bool isAuthorized { get; }
        Enum.APIAuthorizeState AuthorizedState { get; }
        string ClientID { get; set; }
        string Website { get; set; }

        IEnumerable<JB2.Common.IIDNamePair<string,string>> AllowedPointSystems { get; set; }


        JB2.Common.IBusiness Company{ get; set; }

        IEnumerable<JB2.Identity.IPlayer> GetAdmins();

        IEnumerable<IModule> GetModules();
        ApplicationModulePermission GetModulePermission(string moduleID);
        TreasuryRequestKey GetTreasuryRequestKey(string treasuryID);

        string GetDewdropID(string code);
    }
}
