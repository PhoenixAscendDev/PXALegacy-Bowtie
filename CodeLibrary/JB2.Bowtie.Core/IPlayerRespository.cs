using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public interface IBowtiePlayerRespository : JB2.Common.IRepository<JB2.Bowtie.IBowtiePlayer, string>
    {
        BowtieMetadata GetMetaDataByPlayerID(string playerID);

        void Insert(ApplicationPlayer player);

        ApplicationPlayer GetAppPlayerByID(string playerID, string appID);

        IBowtiePlayer GetPlayerByAuth(string authID, string provider);

        ServiceResult InsertAuthInfo(string playerID, AuthInfo authinfo);

        ServiceResult RemoveAuthInfo(string playerID, string authProvider);
       
        
    }
}
