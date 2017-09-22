using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IApplicationRepository : JB2.Common.IRepository<JB2.Bowtie.IApplication, string>
    {
        IApplication[] GetAPIAllowedApps();

        IApplication[] GetApplicationsByClientID(string clientID);

        IApplication GetApplicationByAPIKey(string publicKey);


        string ExportApplicationToJson(IApplication app);

        IApplication ImportApplicationFromJson(string json);

       

       

        //string GetTreasuryRequestKey(string id, string treasuryID);


    }
}
