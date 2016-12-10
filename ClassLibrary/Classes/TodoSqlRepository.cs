using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Interfaces;

namespace ClassLibrary.Classes
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
                throw new ArgumentNullException();
            if (_context.TodoItems.Find(todoItem.Id) != null)
                throw new DuplicateTodoItemException("Duplicate id: {0}", todoItem.Id);
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            if (todoId == null || userId == null || _context.TodoItems.Find(todoId) == null)
            {
                return null;
            }
            if (_context.TodoItems.Find(todoId).UserId != userId)
            {
                throw new TodoAccessDeniedException("You are not the owner of Todo ID: {1}", todoId);
            }
            return _context.TodoItems.Find(todoId);
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            if (userId != null)
            {
                var list = new List<TodoItem>();
                foreach (TodoItem item in _context.TodoItems.ToList())
                {
                    if (item.IsCompleted == false && item.UserId == userId)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            throw new ArgumentNullException();
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            if (userId != null)
            {
                return _context.TodoItems.OrderByDescending(item => item.DateCreated).Where(item => item.UserId == userId).ToList();
            }
            throw new ArgumentNullException();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            if (userId != null)
            {
                var list = new List<TodoItem>();
                foreach (TodoItem item in _context.TodoItems.ToList())
                {
                    if (item.IsCompleted && item.UserId == userId)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            throw new ArgumentNullException();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            if (userId != null)
            {
                return _context.TodoItems.Where(item => item.UserId == userId && filterFunction(item)).ToList();
            }
            throw new ArgumentNullException();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem item = Get(todoId, userId);
            if (item != null)
            {
                bool didItWork = item.MarkAsCompleted();
                if (didItWork) _context.SaveChanges();
                return didItWork;
            }
            return false;

        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem item = Get(todoId, userId);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            TodoItem item = Get(todoItem.Id, userId);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
            }
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }
    }
}
