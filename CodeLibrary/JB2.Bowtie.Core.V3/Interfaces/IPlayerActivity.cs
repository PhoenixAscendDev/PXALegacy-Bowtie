using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IActivityEntry : IBowtieObject, IApplicationable<string>, IPlayerable<string>
    {
        DateTime ActivityDate { get; set; }

        ActivityDataset Data { get; set; }

        string ActivityCode { get; set; }
    }
}
