using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IDewdropTrigger
    {
        bool ShouldWe(IPlayerDewdrop dewdrop);

        JB2.Common.ServiceResult DoTheDew(IPlayerDewdrop dewdrop);
    }
}
