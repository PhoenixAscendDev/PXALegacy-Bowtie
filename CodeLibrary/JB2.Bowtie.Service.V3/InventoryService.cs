using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie;
using JB2.Bowtie.Extensions;

namespace JB2.Bowtie.Service
{
    public class InventoryService : JB2.Common.Singleton<InventoryService>
    {
        #region Fields

        protected IUnitOfWork _uofw;

        #endregion Fields

        #region Constructors
        public InventoryService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {

        }

        public InventoryService(IUnitOfWork unitofwork)
        {
            _uofw = unitofwork;
        }



        #endregion Constructors


        public JB2.Common.ServiceResult<IEnumerable<IInventoryItem>> RetrieveByApplication(IApplication application)
        {
            try
            {

                var data = _uofw.InventoryRepository.GetByApplicationID(application.ID);

                return new JB2.Common.ServiceResult<IEnumerable<IInventoryItem>>(data);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<IEnumerable<IInventoryItem>>();
            }
        }

        public JB2.Common.ServiceResult<IInventoryItem> RetrieveByID(string id)
        {
            try
            {

                var data = _uofw.InventoryRepository.GetById(id);

                return new JB2.Common.ServiceResult<IInventoryItem>(data);
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult<IInventoryItem>();
            }
        }

        public JB2.Common.ServiceResult Save(IInventoryItem entity)
        {
            try
            {
                _uofw.InventoryRepository.Insert(entity);

                return true;
            }
            catch (Exception ex)
            {
                return ex.ToServiceResult();
            }
        }
    }
}
