using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Classes
{
    class TodoAccessDeniedException : Exception
    {
        public string v { get; set; }
        public Guid id { get; set; }

        public TodoAccessDeniedException()
        {
        }

        public TodoAccessDeniedException(string message) : base(message)
        {
        }

        public TodoAccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TodoAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TodoAccessDeniedException(string v, Guid id)
        {
            this.v = v;
            this.id = id;
        }
    }
}
