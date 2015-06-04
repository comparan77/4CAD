using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelCasc.exception
{
    class ExPermission: Exception
    {
        public ExPermission()
            : base() { }

        public ExPermission(string message)
            : base(message) { }

        public ExPermission(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ExPermission(string message, Exception innerException)
            : base(message, innerException) { }

        public ExPermission(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ExPermission(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
