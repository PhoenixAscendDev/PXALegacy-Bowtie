using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Exceptions
{
    public class ApplicationNotInitialized : Exception
    {
        public ApplicationNotInitialized() : this("Application is not Initialized")
        {

        }

        public ApplicationNotInitialized(string message) : this(message,null)
        {

        }

        public ApplicationNotInitialized(string message, Exception innerException) :base(message,innerException)
        {

        }
    }
}
