using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using JB2.Common;

namespace JB2.Bowtie
{
    public class Mission : Achievement, IMission, IClass
    {


        public Mission(string id) : base(id)
        {

        }





        public DateTime ExpireDate
        {
            get
            {
                return _props.GetProperty<DateTime>("ExpireDate",DateTime.MaxValue);
            }
            set
            {
                _props.SetProperty<DateTime>("ExpireDate",value);
            }
        }

        public string NextMissionID
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

        public string PreviousMissionID
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

        public string MissionGroup
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
