using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IDewdropRepository : JB2.Common.IRepository<JB2.Bowtie.Dewdrop, string>
    {
        IEnumerable<Dewdrop> GetByApplicationID(string appID);
        IEnumerable<IPlayerDewdrop> GetPlayerDewsByPlayerID(string playerID);
        IEnumerable<IPlayerDewdrop> GetPlayerDews(string playerID, string dewdropID);
        IEnumerable<IPlayerDewdrop> GetPlayerDewsBySearch(object search);

        void InsertPlayerDew(IPlayerDewdrop playerdew);


        #region DewdropTriggers

        IEnumerable<DewdropTriggerInfo> GetDewdropTriggersByDewdrop(string dewdropID);

        void InsertTriggerInfo(DewdropTriggerInfo info);

        DewdropTriggerInfo GetDewdropTriggerByID(string id);

        void DeleteTriggerInfo(DewdropTriggerInfo info);





        #endregion DewdropTriggers
    }
}
