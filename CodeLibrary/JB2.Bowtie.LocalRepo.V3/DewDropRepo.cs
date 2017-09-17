using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Data.Local
{
    public class DewdropRepo : JB2.Common.Singleton<DewdropRepo>, IDewdropRepository
    {

        #region Fields

        private Dictionary<string, DrewdropData> _playerdata;

        private Dictionary<string, IDewdrop> _dewdrops;

        #endregion Fields

        #region Constructor

        public DewdropRepo()
        {
            if (_playerdata == null)
                _playerdata = new Dictionary<string, DrewdropData>();

            if (_dewdrops == null)
            {
                var json = "[{\"ApplicationID\":\"0\",\"GDID\":\"5E0215BA\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"201\",\"Name\":\"Login\",\"ValueType\":0},{\"ApplicationID\":\"0\",\"GDID\":\"604815E2\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"202\",\"Name\":\"ButtonClick\",\"ValueType\":0},{\"ApplicationID\":\"0\",\"GDID\":\"B1D61720\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"203\",\"Name\":\"LastLoginDay\",\"ValueType\":1},{\"ApplicationID\":\"0\",\"GDID\":\"773E1657\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"204\",\"Name\":\"LastLoginTime\",\"ValueType\":1},{\"ApplicationID\":\"0\",\"GDID\":\"863A1626\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"205\",\"Name\":\"AddToInventory\",\"ValueType\":0},{\"ApplicationID\":\"0\",\"GDID\":\"A8D616A4\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"206\",\"Name\":\"MinutesPlayed\",\"ValueType\":0}]";
                var dewdropList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dewdrop>>(json);
                _dewdrops = new Dictionary<string, IDewdrop>();
                foreach(var d in dewdropList)
                {
                    _dewdrops.Add(d.GDID, d);
                }

            }


        }

        #endregion Constructor


        #region Dewdrop
        public void Delete(IDewdrop entity)
        {
            throw new NotImplementedException();
        }

        public IDewdrop[] GetAll()
        {
            return GetAll(null);
        }

        public IDewdrop[] GetAll(int? maxRecordCount)
        {
            return getall().Take(maxRecordCount == null ? 250 : (int)maxRecordCount).ToArray();
        }

        public IEnumerable<IDewdrop> GetByApplicationID(string appID)
        {
            return getall().Where(x => x.ApplicationID == appID);
        }

        public IDewdrop GetByGDID(string gdid)
        {
            return getall().Where(x => x.GDID == gdid).FirstOrDefault();
        }

        public IDewdrop GetById(string id)
        {
            return getall().Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(IDewdrop entity)
        {
            throw new NotImplementedException();
        }

        public IDewdrop[] SearchFor()
        {
            throw new NotImplementedException();
        }

        public IDewdrop[] SearchFor(string filter)
        {
            throw new NotImplementedException();
        }

        #endregion Dewdrop


        #region DewdropDataSet

        public JB2.Bowtie.DrewdropData GetDataByPlayer(string applicationID, string playerID)
        {
            string key = applicationID + ">*<" + playerID;
            var result = _playerdata[key];
            return result;
        }

        public JB2.Common.ServiceResult InsertDewdropData(string applicationID, string playerID, DrewdropData data)
        {

            try
            {
                string key = applicationID + ">*<" + playerID;

                _playerdata[key] = data;

                return true;
            }
            catch(Exception ex)
            {
                return new Common.ServiceResult(ex);
            }
        }


        #endregion DewdropDataSet

        #region Helpers

        public IEnumerable<IDewdrop> getall()
        {
            //var json = "[{\"ApplicationID\":\"0\",\"GDID\":\"5E0215BA\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"201\",\"Name\":\"Login\",\"ValueType\":0},{\"ApplicationID\":\"0\",\"GDID\":\"604815E2\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"202\",\"Name\":\"ButtonClick\",\"ValueType\":0},{\"ApplicationID\":\"0\",\"GDID\":\"B1D61720\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"203\",\"Name\":\"LastLoginDay\",\"ValueType\":1},{\"ApplicationID\":\"0\",\"GDID\":\"773E1657\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"204\",\"Name\":\"LastLoginTime\",\"ValueType\":1},{\"ApplicationID\":\"0\",\"GDID\":\"863A1626\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"205\",\"Name\":\"AddToInventory\",\"ValueType\":0},{\"ApplicationID\":\"0\",\"GDID\":\"A8D616A4\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":\"206\",\"Name\":\"MinutesPlayed\",\"ValueType\":0}]";
            //return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dewdrop>>(json);

            return _dewdrops.Values.ToList();


        }




        #endregion Helpers
    }
}
