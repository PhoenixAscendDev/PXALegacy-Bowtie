using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

using JB2.Bowtie;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie.Web.Models
{
    public class GraphElementViewModel : BaseViewModel
    {
        public string ApplicationID { get; set; }
       

        public string ElementType { get; set; }
               

        public string ParentID { get; set; }

        public IEnumerable<SelectListItem> GetGraphPropertyTypeSelectList()
        {
            Array values = System.Enum.GetValues(typeof(Enum.GraphPropertyType));
            List<SelectListItem> items = new List<SelectListItem>(values.Length);

            foreach (var i in values)
            {
                items.Add(new SelectListItem
                {
                    Text = System.Enum.GetName(typeof(Enum.GraphPropertyType), i),
                    Value = System.Enum.GetName(typeof(Enum.GraphPropertyType), i),
                });
            }

            return items;


        }

        public IEnumerable<SelectListItem> GetGraphPropertySelectList()
        {

            return GetGraphPropertySelectList(false);
        }

        public IEnumerable<SelectListItem> GetGraphPropertySelectList(bool includeheader, string headerText = "Select Property")
        {

            var list = BaseViewModel.GraphPropertiesSelectList().ToList();
            return list;        
        }

        public IEnumerable<SelectListItem> GetGraphObjectSelectList()
        {
            return GetGraphObjectSelectList(false);
        }

        public IEnumerable<SelectListItem> GetGraphObjectSelectList(bool includeheader, string headerText = "Select Graph Objects")
        {
            var list = BaseViewModel.GraphObjectsSelectList().ToList();
            return list;
        }

    }
}