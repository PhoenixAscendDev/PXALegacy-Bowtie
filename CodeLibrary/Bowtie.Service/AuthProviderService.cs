using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class AuthProviderService : GenericService<IAuthProvider, IAuthProviderRepository>
    {

        #region Constructors

        public AuthProviderService() : base()
        {

        }

        public AuthProviderService(IUnitOfWork unitOfWork) : base(unitOfWork.AuthProviderRepository)
        {
            _uofw = unitOfWork;
        }

        #endregion Constructors

        public ProfileResourcePacket GetProfilePacket(AuthInfo authInfo)
        {
            //1. Get Info from Auth Provider
            //2. If error (timeout..etc), use backup data from the player repo
            return new ProfileResourcePacket();
        }

    }
}
