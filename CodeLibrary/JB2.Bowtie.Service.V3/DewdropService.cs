using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

using JB2.Bowtie.Extensions;

namespace JB2.Bowtie.Service
{
    public class DewdropService : JB2.Common.Singleton<DewdropService>
    {
        #region Fields

        protected JB2.Bowtie.IUnitOfWork _uofw;

        #endregion Fields


        #region Constructors

        public DewdropService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {
            
        }

        public DewdropService(JB2.Bowtie.IUnitOfWork unitofWork)
        {
            _uofw = unitofWork;
        }

        #endregion Constructors



        public JB2.Common.ServiceResult<IEnumerable<IDewdrop>> RetrieveByApplication(IApplication application)
        {
            try
            {
                var list = _uofw.DewdropRepository.GetByApplicationID(application.ID);
                return new JB2.Common.ServiceResult<IEnumerable<IDewdrop>>(list);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<IEnumerable<IDewdrop>>();
            }
        }

        public JB2.Common.ServiceResult<JB2.Bowtie.DewdropData> RetrieveDewdropData(IPlayer player, IApplication application)
        {
            try
            {
                var playerID = player.ID;
                var applicationID = application.ID;
                return _uofw.DewdropRepository.GetDataByPlayer(applicationID, playerID);

            }
            catch(Exception ex)
            {
                return new Common.ServiceResult<DewdropData>(ex);
            }
            
        }

        public JB2.Common.ServiceResult<IEnumerable<IDewdropEntry>> RetrieveDewdropLog(IPlayer player, IApplication application)
        {
            try
            {
                var list = _uofw.DewdropRepository.GetDewdropLog(application.ID, player.ID);
                return new JB2.Common.ServiceResult<IEnumerable<IDewdropEntry>>(list);
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<IEnumerable<IDewdropEntry>>();
            }
        }

        public JB2.Common.ServiceResult<int> RetrieveDewdropValue(IPlayer player, IApplication application, string GDID)
        {
            try
            {
                var dataset = _uofw.DewdropRepository.GetDataByPlayer(application.ID, player.ID);
                var dewdrops = _uofw.DewdropRepository.GetByApplicationID(application.ID);
                var dewdrop = dewdrops.Where(x => x.GDID == GDID).FirstOrDefault();

                if (dataset == null)
                {
                    dataset = this.GenerateDewdropData(player, application);
                }

                int result = dataset.GetDewDropValue(dewdrop.ID);

                return new Common.ServiceResult<int>(result);

            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<int>();
            }
            

        }


        public JB2.Common.ServiceResult<IEnumerable<IDewdropEntry>> RetrieveDewdropLogByPlayer(IPlayer player)
        {
            try
            {
                var apps = _uofw.ApplicationRepository.GetAll();

                var result = new List<IDewdropEntry>();

                foreach(var a in apps)
                {
                    var list = _uofw.DewdropRepository.GetDewdropLog(a.ID, player.ID);
                    result.AddRange(list);
                }

                return new Common.ServiceResult<IEnumerable<IDewdropEntry>>(result);

            }
            catch(Exception ex)
            {
                return ex.ToServiceResult<IEnumerable<IDewdropEntry>>();
            }

        }

        public JB2.Common.ServiceResult<IDewdropEntry> AddDewdropEntry(IPlayer player, IApplication application, ushort value,string message,string GDID)
        {
            try
            {
                var playerID = player.ID;
                var applicationID = application.ID;
                var dewdrops = _uofw.DewdropRepository.GetByApplicationID(applicationID);

                //var dewdrop = dewdrops.Where(x => x.GDID == GDID).First();

                var dataset = _uofw.DewdropRepository.GetDataByPlayer(applicationID, playerID);

                if(dataset == null)
                {
                    dataset = this.GenerateDewdropData(player,application);
                }

                var dateSubmit = System.DateTime.UtcNow;
                var parent = GDID;

                
                while(parent != 0.ToString())
                {
                    var dewdrop = dewdrops.Where(x => x.GDID == parent).FirstOrDefault();

                    switch (dewdrop.ValueType)
                    {
                        case Enum.DewDropValueType.Count:
                            dataset.IncrementDewDrop(dewdrop.ID);
                            break;
                        case Enum.DewDropValueType.Flags:
                            dataset.SetDewdropValue(dewdrop.ID, value);
                            break;
                    }

                    parent = dewdrop.ParentGDID;
                }


                

                DewdropEntry e = new DewdropEntry();
                

                e.ApplicationID = applicationID;
                e.GDID = GDID;
                e.PlayerID = playerID;
                e.SubmittedDate = dateSubmit;
                e.Value = dataset.GetDewDropValue(dewdrops.Where(x => x.GDID == GDID).First().ID);

                _uofw.DewdropRepository.Insert(e);

                _uofw.DewdropRepository.InsertDewdropData(applicationID, playerID, dataset);

                JB2.Events.Bowtie.OnDewdropDataUpdated(dataset, e);

                return new JB2.Common.ServiceResult<IDewdropEntry>(e);
            }
            catch(Exception ex)
            {
                return new JB2.Common.ServiceResult<IDewdropEntry>(ex);
            };
        }

        public JB2.Common.ServiceResult SaveDewdropData(IPlayer player, IApplication application, DewdropData data)
        {
            try
            {
                var playerID = player.ID;
                var applicationID = application.ID;
                _uofw.DewdropRepository.InsertDewdropData(applicationID, playerID, data);
                return true;
            }
            catch(Exception ex)
            {
                return new Common.ServiceResult(ex);
            }
        }

        public DewdropData GenerateDewdropData(IPlayer player, IApplication application)
        {
            var playerID = player.ID;
            var applicationID = application.ID;
            var dd = DewdropData.Empty;

            _uofw.DewdropRepository.InsertDewdropData(applicationID, playerID, dd);

            return dd;

        }

        public JB2.Common.ServiceResult Save(IDewdrop dewdrop)
        {
            try
            {
                _uofw.DewdropRepository.Insert(dewdrop);

                return true;
            }
            catch(Exception ex)
            {
                return ex.ToServiceResult();
            }
        }
    }
}
