using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class JbeanTreasuryNote : TreasuryNote
    {
        public JbeanTreasuryNote(string id,long amount, JB2.Identity.IApplication app)
        {
            _id = id;
            _amount = amount;
            _requestor = app;
        }


        public static JbeanTreasuryNote NewNote(long amount, JB2.Identity.IApplication requestor)
        {
            var note = new JbeanTreasuryNote(JB2.Common.NewID.Guid(), amount, requestor);
            return note;
        }
    }
}
