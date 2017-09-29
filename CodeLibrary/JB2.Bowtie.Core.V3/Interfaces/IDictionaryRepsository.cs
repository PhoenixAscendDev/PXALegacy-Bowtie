using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IDictionaryRepository
    {
        Dictionary<string, string> GetApplicationActivities(string applicationID);

        void InsertActivities(string applicationID, Dictionary<string, string> activities);
    }
}
