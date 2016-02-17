using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Data
{
    public class ObjectNotFoundInRepositoryException : System.ApplicationException
    {
        private string stackTraceOverride;
        private object _entityType;
        private string _entityId;

        public ObjectNotFoundInRepositoryException(string entityId = "", object respository = null)
                : this(null,entityId,respository)
        {

        }

        public ObjectNotFoundInRepositoryException(string message, string entityId = "", object respository = null)
                : this(null,message,entityId, respository)
        {
        }

        public ObjectNotFoundInRepositoryException(Exception innerException, string message = "entity not found in the supplied respository", string entityId = "", object respository = null)
                : base(message, innerException)
        {
            _entityType = respository;
            _entityId = entityId;
        }

        public void SetStackTrace(string stackTrace)
        {
            var lines = new List<string>(stackTrace.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            while (lines.Count > 0 && lines[0].IndexOf(".LogMessage(") > 0)
            {
                lines.RemoveAt(0);
            }

            this.stackTraceOverride = String.Join("\r\n", lines.ToArray());
        }


        public object Respository
        {
            get
            {
                return _entityType;
            }
        }

        public string EntityID
        {
            get
            {
                return _entityId;
            }
        }


        public override string StackTrace
        {
            get
            {
                return this.stackTraceOverride ?? base.StackTrace;
            }
        }

    }
}
