using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

using JB2.Common.Data;

namespace JB2.Bowtie.Queue.Azure
{
    public class DewdropQueue : BaseQueue, IDewdropQueueRepo
    {

        #region Constructor
        public DewdropQueue(AzureQueueRepository repo) : base(repo)
        {

        }

        public ServiceResult PushDewdrop(IPlayerDewdrop pdewdrop )
        {
            var json = @"{'PlayerID':'" + pdewdrop.GetPlayerID() + "','DewdropID':'" + pdewdrop.GetDewdropID() + "','ID':'" +  pdewdrop.GetID() + "','Value':'" + pdewdrop.GetValue() + "'}";

            return this.Push(json.Replace("'", "\""));
        }


        #endregion Constructor
    }
}
