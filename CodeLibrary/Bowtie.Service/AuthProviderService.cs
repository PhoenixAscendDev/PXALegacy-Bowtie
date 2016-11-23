using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Service
{
    public class AuthProviderService : GenericService<IAuthProvider,IAuthProviderRepository>
    {
        ProfileResourcePacket  GetProfilePacket(AuthInfo authInfo)
        {
            //1. Get Info from Auth Provider
            //2. If error (timeout..etc), use backup data from the player repo
            return new ProfileResourcePacket();
        }
    }
}
