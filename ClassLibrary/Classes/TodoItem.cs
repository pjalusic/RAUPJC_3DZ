using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Classes
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }

        /// <summary >
        /// Nullable date time .
        /// DateTime is value type and won ’t allow nulls .
        /// DateTime ? is nullable DateTime and will accept nulls .
        /// Use null when to do completed date does not exist (e.g. to do is still
        /// not completed )
        ///  </summary >
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        /// <summary >
        /// User id that owns this TodoItem
        /// </summary >
        public Guid UserId { get; set; }

        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid(); // Generates new unique identifier
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now; // Set creation date as current time
            DateCompleted = null;
            UserId = userId;
        }

        public TodoItem()
        {
            // entity framework needs this one
            // not for use :)
        }
        
        public bool MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }

        /*
        private sealed class IdEqualityComparer : IEqualityComparer<TodoItem>
        {
            public bool Equals(TodoItem x, TodoItem y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(TodoItem obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        private static readonly IEqualityComparer<TodoItem> IdComparerInstance = new IdEqualityComparer();

        public static IEqualityComparer<TodoItem> IdComparer
        {
            get { return IdComparerInstance; }
        }
        */
    }
}
