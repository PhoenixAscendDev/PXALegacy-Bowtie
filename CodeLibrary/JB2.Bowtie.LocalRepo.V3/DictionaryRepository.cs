using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Data.Local
{
    public class DictionaryRepository : JB2.Common.Singleton<DictionaryRepository>, IDictionaryRepository
    {
        #region Fields


        Dictionary<string, Dictionary<string, string>> _activities;

        #endregion Fields

        #region Construction

        public DictionaryRepository()
        {

        }

       

        #endregion Construction

        public Dictionary<string, string> GetApplicationActivities(string applicationID)
        {
            if (_activities.ContainsKey(applicationID))
                return _activities[applicationID];
            else
                return new Dictionary<string, string>();

        }

        public void InsertActivities(string applicationID, Dictionary<string, string> activities)
        {
            if (!_activities.ContainsKey(applicationID))
                _activities.Add(applicationID, activities);
            else
                _activities[applicationID] = activities;
        }
    }
}
