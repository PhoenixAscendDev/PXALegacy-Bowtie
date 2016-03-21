using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy.Exceptions
{
    public class IssueJBeanProhibited : Exception
    {
        public IssueJBeanProhibited()
            : this("Application is prohibited from issuing jBean Tokens at this time")
        {

        }

        public IssueJBeanProhibited(string message): this(message,null)
        {

        }

        public IssueJBeanProhibited(string message,Exception innerException): base(message,innerException)
        {

        }
    }
}
