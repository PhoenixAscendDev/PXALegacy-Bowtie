using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IMission : IAchievement
    {
        DateTime ExpireDate { get; set; }
        string NextMissionID { get; set; }
        string PreviousMissionID { get; set; }
        string MissionGroup { get; set; }



    }
}
