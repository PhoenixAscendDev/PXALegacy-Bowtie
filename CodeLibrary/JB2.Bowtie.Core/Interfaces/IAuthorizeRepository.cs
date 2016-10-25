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

        JB2.Common.ServiceResult SaveAuthorizeKey(string key, string applicationID);

        JB2.Common.ServiceResult Save(ApplicationStatePair pair);

        JB2.Common.ServiceResult Save(Enum.APIAuthorizeState state, string applicationID);


    }
}
