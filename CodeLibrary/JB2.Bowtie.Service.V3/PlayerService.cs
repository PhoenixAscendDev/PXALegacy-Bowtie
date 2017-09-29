using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Extensions;
using JB2.Common;

namespace JB2.Bowtie.Service
{
    public class PlayerService : JB2.Common.Singleton<PlayerService>
    {
        #region Fields

        protected IUnitOfWork _uofw;

        #endregion Fields

        #region Constructors
        public PlayerService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public PlayerService(IUnitOfWork unitofwork)
        {
            _uofw = unitofwork;
        }

        #endregion Constructors


        public JB2.Common.ServiceResult<IPlayer> RetrievePlayerById(string id)
        {
            try
            {
                var app = _uofw.PlayerRepository.GetById(id);
                return new ServiceResult<IPlayer>(app);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<IPlayer>();
            }
        }

        public JB2.Common.ServiceResult Save(IPlayer p)
        {
            try
            {
                _uofw.PlayerRepository.Insert(p);

                return true;
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult();
            }
        }
    
    }
}
