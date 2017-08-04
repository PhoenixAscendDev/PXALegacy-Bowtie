using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using JB2.Common;

namespace JB2.Bowtie
{
    public class Task : Achievement, IWorkflowTask, IClass
    {
        public DateTime ExpireDate
        {
            get
            {
                return _props.GetProperty<DateTime>("ExpireDate");
            }
            set
            {
                _props.SetProperty<DateTime>("ExpireDate",value);
            }
        }

        public string NextTaskID
        {
            get
            {
                return _props.GetProperty<string>("NextTaskID");
            }

            set
            {
                _props.SetProperty<string>("NextTaskID",value);
            }
        }

        public string PreviousTaskID
        {
            get
            {
                return _props.GetProperty<string>("PreviousTaskID");
            }

            set
            {
                _props.SetProperty<string>("PreviousTaskID", value);
            }
        }

        public string WorkflowID
        {
            get
            {
                return _props.GetProperty<string>("Workflow");
            }

            set
            {
                _props.SetProperty<string>("Workflow",value);
            }
        }
    }
}
