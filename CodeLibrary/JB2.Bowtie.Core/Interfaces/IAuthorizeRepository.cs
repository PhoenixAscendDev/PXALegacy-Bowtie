using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IAuthorizeRepository
    {
        ApplicationStatePair GetApplicationStateByAPIKey(string publicKey, string secret);

        ApplicationStatePair GetApplicationStateByID(string id);

        ApplicationStatePair GetApplicationStateByAuthorizeKey(string id);

        long GetAuthorizeKeyTicks(string authorizeKey);

        JB2.Common.ServiceResult InsertAuthorizeKey(string key, string applicationID, DateTime dateGenerated);

        JB2.Common.ServiceResult Insert(ApplicationStatePair pair);

        JB2.Common.ServiceResult UpdateAuthorizeState(Enum.APIAuthorizeState state, string applicationID);


    }
}
