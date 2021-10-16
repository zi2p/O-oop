using System;
using System.Runtime.Serialization;

namespace IsuExtra.Tools
{
    public class OGNPException : Exception
    {
        public OGNPException()
        {
        }

        public OGNPException(string message)
            : base(message)
        {
        }

        public OGNPException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected OGNPException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}