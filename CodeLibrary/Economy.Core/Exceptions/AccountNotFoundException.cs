using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy.Exceptions
{
    public class AccountNoteFoundException : Exception
    {
        public AccountNoteFoundException()
            : this("Account Not Found")
        {

        }
        
        public AccountNoteFoundException(string message) : this(message, null)
        {

        }

        public AccountNoteFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
