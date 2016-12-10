using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Classes
{
    [Serializable]
    class DuplicateTodoItemException : Exception
    {
        private Guid id;
        private string v;

        public DuplicateTodoItemException()
        {
        }

        public DuplicateTodoItemException(string message) : base(message)
        {
        }

        public DuplicateTodoItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicateTodoItemException(string v, Guid id)
        {
            this.v = v;
            this.id = id;
        }

        protected DuplicateTodoItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
