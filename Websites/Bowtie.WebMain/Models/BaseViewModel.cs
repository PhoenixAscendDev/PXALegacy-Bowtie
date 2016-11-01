using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using JB2.Bowtie.Service;
using JB2.Bowtie.Web;

namespace JB2.Bowtie.Web.Models
{
    public class BaseViewModel : JB2.Common.IDNamePair<string, string>
    {

        public virtual IEnumerable<SelectListItem> GetApplicationSelectList(string headerText = "Select Application")
        {
            var list = BaseViewModel.ApplicationSelectList().ToList();
            list.Insert(0, new SelectListItem() { Text = headerText, Value = "-1" });
            list.Insert(1, new SelectListItem() { Text = "-------------", Value = "-1" });
            list.Insert(2, new SelectListItem() { Text = "-N/A-", Value = "" });

            return list;
        }


        #region Static
        protected static IUnitOfWork _unitOfWork;
        public static IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork != null)
                    return JB2.Settings.Bowtie.UnitOfWork;
                else
                    return _unitOfWork;
                
            }
            set
            {
                _unitOfWork = value;
            }
        }

        public static IEnumerable<SelectListItem> ApplicationSelectList(IUnitOfWork uofw)
        {          
            var service = new ApplicationService(uofw);
            var appsAll = service.Retrieve();
            return appsAll.ToSelectItems();
        }


        public static IEnumerable<SelectListItem> GraphObjectsSelectList(IUnitOfWork uofw)
        {
            var service = new GraphService(uofw);
            var all = service.RetrieveObjects();
            return all.ToSelectItems();
        }

        public static IEnumerable<SelectListItem> GraphPropertiesSelectList(IUnitOfWork uofw)
        {
            var service = new GraphService(uofw);
            var all = service.RetreiveProperties();
            return all.ToSelectItems();
        }

        public static IEnumerable<SelectListItem> GraphActionSelectList(IUnitOfWork uofw)
        {
            var service = new GraphService(uofw);
            var all = service.RetrieveActions();
            return all.ToSelectItems();
        }



        public static IEnumerable<SelectListItem> ApplicationSelectList()
        {
            return ApplicationSelectList(BaseViewModel.UnitOfWork);
        }

        public static IEnumerable<SelectListItem> GraphActionSelectList()
        {
            return GraphActionSelectList(BaseViewModel.UnitOfWork);
        }

        public static IEnumerable<SelectListItem> GraphObjectsSelectList()
        {
            return GraphObjectsSelectList(BaseViewModel.UnitOfWork);
        }

        public static IEnumerable<SelectListItem> GraphPropertiesSelectList()
        {
            return GraphPropertiesSelectList(BaseViewModel.UnitOfWork);
        }

        #endregion Static
    }
}