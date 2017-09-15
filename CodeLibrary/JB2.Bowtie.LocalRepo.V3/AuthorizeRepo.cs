using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie.Data.Local
{
    public class AuthorizeRepo : JB2.Common.Singleton<AuthorizeRepo>, IAuthorizeRepository
    {
        public ApplicationStatePair GetApplicationStateByAPIKey(string publicKey, string secret)
        {
            ApplicationStatePair asp = new ApplicationStatePair("1", APIAuthorizeState.Authorized);
            return asp;
        }

        public ApplicationStatePair GetApplicationStateByAuthorizeKey(string id)
        {
            ApplicationStatePair asp = new ApplicationStatePair("1", APIAuthorizeState.Authorized);
            return asp;
        }

        public ApplicationStatePair GetApplicationStateByID(string id)
        {
            ApplicationStatePair asp = new ApplicationStatePair("1", APIAuthorizeState.Authorized);
            return asp;
        }

        public long GetAuthorizeKeyTicks(string authorizeKey)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(ApplicationStatePair pair)
        {
            throw new NotImplementedException();
        }

        public ServiceResult InsertAuthorizeKey(string key, string applicationID, DateTime dateGenerated)
        {
            throw new NotImplementedException();
        }

        public ServiceResult UpdateAuthorizeState(APIAuthorizeState state, string applicationID)
        {
            throw new NotImplementedException();
        }

        #region Helpers

        

        #endregion Helpers
    }
}
