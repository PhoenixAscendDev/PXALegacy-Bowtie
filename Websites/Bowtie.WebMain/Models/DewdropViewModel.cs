using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel.DataAnnotations;

namespace JB2.Bowtie.Web.Models
{
    public class DewdropViewModel : BaseViewModel
    {
        [Display(Name = "Application")]
        public string ApplicationID { get; set; }
        [Display(Name = "Action ")]
        public string GraphID { get; set; }
        public string Description { get; set; }
        public int jBeanCost { get; set; }  

        public IEnumerable<SelectListItem> GetGraphActionsSelectList(string headerText = "Select Graph Actions")
        {
            var list = BaseViewModel.GraphActionSelectList().ToList();
            list.Insert(0, new SelectListItem() { Text = headerText, Value = "-1" });
            list.Insert(1, new SelectListItem() { Text = "-------------", Value = "-1" });
            list.Insert(2, new SelectListItem() { Text = "-N/A-", Value = "" });
            return list;
        }
    }
}