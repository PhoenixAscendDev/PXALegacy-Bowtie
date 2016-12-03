using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie.Service
{
    public class PointSystemService : JB2.Common.IObjectService<IPointSystem,bool, string>
    {
        protected IPointSystemRepository _repo;
        protected IUnitOfWork _uofw;


        #region Constructors
        public PointSystemService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public PointSystemService(IUnitOfWork unitOfWork) : this(unitOfWork.PointSystemRepository)
        {
            _uofw = unitOfWork;
        }

        public PointSystemService(IPointSystemRepository repo)
        {
            _repo = repo;

        }

        #endregion Constructors


        public List<PointSystemConfig> RetrieveConfigs()
        {
            return _repo.GetAllConfigs().ToList();
        }

        public PointSystemConfig RetrieveConfigById(string id)
        {
            return _repo.GetAllConfigs().ToList().Find(x => x.ID == id);
        }


        public virtual bool Remove(IPointSystem entity)
        {
            _repo.Delete(entity);
            return true;
        }

        public virtual List<IPointSystem> Retrieve(string request)
        {
            throw new NotImplementedException();
        }

        public virtual List<IPointSystem> Retrieve(bool isActive)
        {
            throw new NotImplementedException();
        }

        public virtual List<IPointSystem> Retrieve()
        {
            try
            {
                return _repo.GetAll().ToList();
            }
            catch (Exception ex)
            {
                ex.BowtieLog();
                return new List<IPointSystem>();
            }

        }

        public virtual IPointSystem RetrieveById(string id)
        {
            return _repo.GetById(id);
        }

        public virtual IPointSystem RetrieveByName(string name)
        {
            throw new NotImplementedException();
        }

        public virtual bool Save(IPointSystem entity)
        {
            _repo.Insert(entity);
            return true;
        }


        #region Player Data

        public int RetrievePlayerPointTotal(string pointsystemID)
        {
            return 0;
        }

        public PointTransaction AddPointsToPlayer(PlayerPoint playerPoint, IPointGiver pointGiver, bool addToQueue = true)
        {
            PointTransaction tran = null;
            try
            {
                tran = PointTransaction.FromPlayerPointGiver(playerPoint, pointGiver);
                tran.ValidationKey = GenerateValidationKey(tran);

                _repo.Insert(tran);

                if (addToQueue)
                    _uofw.PointQueue.PushPointTran(tran);
                else
                    Process(tran);
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
            }

            return tran;
        }

        public string GenerateValidationKey(PointTransaction pt, string hashkey = null)
        {
            //eventually based on application
            if (hashkey == null)
                hashkey = JB2.Common.RNG.D4.ToString();
            var urlString = JB2.Configuration.GetAppSetting("JB2: UrlHash:BowtiePointTranID-" + hashkey.ToString());
            var key = JB2.Common.NewID.UriHash(new Uri(
                                string.Format(urlString,
                                                pt.GiverID + pt.PlayerID,
                                                pt.GiverID + pt.Points.ToString(),
                                                pt.TransactionDate.Ticks.ToString()
                                              )
                                      )
                                );
            return hashkey + key;
        }
        public bool Validate(PointTransaction pt)
        {        
            var key2 = this.GenerateValidationKey(pt, pt.ValidationKey.Substring(0,1));
            pt.IsValid = key2 == pt.ValidationKey;

            return pt.IsValid;
        }

        public ServiceResult Process(PointTransaction pt)
        {
            try
            {
                //get the Point System
                var pservice = new PointSystemService(_uofw);


                var pointSystem = pservice.RetrieveById(pt.PointSystem);

                if(pointSystem != null)
                    pointSystem.Process(pt);
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return new ServiceResult(ex);
            }


            //mark it as processed and update the index
            pt.ProcessFlag = System.DateTime.Now.Ticks.ToString();
            _repo.Insert(pt);
            _repo.UpdateIndex(pt);

            return true;
        }

        #endregion Player Data


    }
}
