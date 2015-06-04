using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelCasc.exception
{
    public class ImportException : Exception
    {
        public ImportException()
            : base() { }

        public ImportException(string message)
            : base(message) { }

        public ImportException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ImportException(string message, Exception innerException)
            : base(message, innerException) { }

        public ImportException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ImportException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
