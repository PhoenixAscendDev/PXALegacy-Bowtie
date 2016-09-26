using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class InvalidChecksumException : Exception
    {
        public InvalidChecksumException()
            : this("Object's Checksum is invalid")
        {

        }
        
        public InvalidChecksumException(string message) : this(message, null)
        {

        }

        public InvalidChecksumException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
