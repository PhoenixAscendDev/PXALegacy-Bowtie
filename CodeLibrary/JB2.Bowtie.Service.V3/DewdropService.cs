using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

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
    }
}
