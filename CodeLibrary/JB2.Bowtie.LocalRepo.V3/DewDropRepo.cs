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

        private Dictionary<string, DewdropData> _playerdata;

        private Dictionary<string, ulong> _playerdataKeys;

        private Dictionary<string, IDewdrop> _dewdrops;

        private Dictionary<string, List<IDewdropEntry>> _dewdropsLog;

        #endregion Fields

        #region Constructor

        public DewdropRepo()
        {
            if (_playerdata == null)
            {
                _playerdata = new Dictionary<string, DewdropData>();
                _playerdataKeys = new Dictionary<string, ulong>();
            }

            if (_dewdrops == null)
            {
                var json = "[{\"ApplicationID\":\"0\",\"GDID\":\"A07A16EE\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":201,\"Name\":\"Login\",\"ValueType\":0},{\"ApplicationID\":\"0\",\"GDID\":\"8D38169B\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":202,\"Name\":\"ButtonClick\",\"ValueType\":0},{\"ApplicationID\":\"0\",\"GDID\":\"8676164D\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":203,\"Name\":\"LastLoginDay\",\"ValueType\":1},{\"ApplicationID\":\"0\",\"GDID\":\"90FC16DB\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":204,\"Name\":\"LastLoginTime\",\"ValueType\":1},{\"ApplicationID\":\"0\",\"GDID\":\"77B61679\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":205,\"Name\":\"AddToInventory\",\"ValueType\":0},{\"ApplicationID\":\"0\",\"GDID\":\"5A6015BF\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":206,\"Name\":\"MinutesPlayed\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"9B7616C6\",\"ParentGDID\":\"8D38169B\",\"IsActive\":true,\"ID\":1,\"Name\":\"Vote\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"708C1604\",\"ParentGDID\":\"9B7616C6\",\"IsActive\":true,\"ID\":2,\"Name\":\"ThisVote\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"A51816AC\",\"ParentGDID\":\"9B7616C6\",\"IsActive\":true,\"ID\":3,\"Name\":\"ThatVote\",\"ValueType\":0},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"DDB01769\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":4,\"Name\":\"LastVoteDate\",\"ValueType\":2},{\"ApplicationID\":\"9F0199E\",\"GDID\":\"BFC2171B\",\"ParentGDID\":\"0\",\"IsActive\":true,\"ID\":5,\"Name\":\"ConsecutiveVotes\",\"ValueType\":1}]";
                var dewdropList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BasicDewdrop>>(json);
                _dewdrops = new Dictionary<string, IDewdrop>();
                foreach (var d in dewdropList)
                {
                    _dewdrops.Add(d.GDID, d);
                }

            }

            if (_dewdropsLog == null)
            {
                _dewdropsLog = new Dictionary<string, List<IDewdropEntry>>();
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

            List<IDewdrop> list = new List<IDewdrop>(250);

            list.AddRange(getall().Where(x => x.ApplicationID == appID));
            list.AddRange(getglobal());

            return list;

        }

        public IDewdrop GetByGDID(string gdid)
        {
            return getall().Where(x => x.GDID == gdid).FirstOrDefault();
        }


        public IDewdrop GetById(string applicationid, byte id)
        {
            return getall().Where(x => x.ID == id && x.ApplicationID == applicationid).FirstOrDefault();
        }
        public IDewdrop GetById(string id)
        {
            return getall().Where(x => x.GDID == id).FirstOrDefault();
        }

        public void Insert(IDewdrop entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(IDewdropEntry entity)
        {
            var playerID = entity.PlayerID;
            var applicationID = entity.ApplicationID;

            string key = applicationID + ">*<" + playerID;

            if (!_dewdropsLog.ContainsKey(key) || _dewdropsLog[key] == null)
            {
                List<IDewdropEntry> list = new List<IDewdropEntry>();

                _dewdropsLog[key] = list;
            }

            _dewdropsLog[key].Add(entity);



        }

        public IEnumerable<IDewdropEntry> GetDewdropLog(string applicationID, string playerID)
        {
            string key = applicationID + ">*<" + playerID;

            if (_dewdropsLog.ContainsKey(key))
                return _dewdropsLog[key];
            else
                return new IDewdropEntry[0];
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

        public JB2.Bowtie.DewdropData GetDataByPlayer(string applicationID, string playerID)
        {
            string key = applicationID + ">*<" + playerID;
            DewdropData result = null;
            if (_playerdata.ContainsKey(key))
                result = _playerdata[key];
            else
                result = null;

            return result;
        }

        public JB2.Common.ServiceResult InsertDewdropData(string applicationID, string playerID, DewdropData data)
        {

            try
            {
                if (!data.isValid)
                    throw new Exception("DewdropData is not valid");

                string key = applicationID + ">*<" + playerID;

                if(_playerdataKeys.ContainsKey(key))
                {
                    var playerDataID = _playerdataKeys[key];

                    var verify = data.DewDropDataID;

                    if (verify == playerDataID)
                    {
                        _playerdata[key] = data;
                    }
                    else
                    {
                        throw new Exception("DewdropDataID does not match the one saved in the records");
                    }

                }
                else
                {
                    _playerdata[key] = data;
                    _playerdataKeys[key] = data.DewDropDataID;
                }
                



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

        public IEnumerable<IDewdrop> getglobal()
        {
            return _dewdrops.Values.Where(x => x.ApplicationID == 0.ToString());
        }






        #endregion Helpers
    }
}
