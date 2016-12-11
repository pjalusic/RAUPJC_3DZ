using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Classes;
using ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAplikacija.Models;
using WebAplikacija.Models.TodoViewModels;

namespace WebAplikacija.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        // Inject user manager into constructor
        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            Guid userId = await Task.Run(YourAction);
            var todos = _repository.GetActive(userId);
            return View(todos);
        }
        
        
        public async Task<IActionResult> Completed()
        {
            Guid userId = await Task.Run(YourAction);
            var todos = _repository.GetCompleted(userId);
            return View(todos);
        }

        [HttpGet("Todo/MarkAsCompleted/{id}")]
        public async Task<IActionResult> MarkAsCompleted(Guid id)
        {
            Guid userId = await Task.Run(YourAction);
            _repository.MarkAsCompleted(id, userId);
            return RedirectToAction("Index");
        }

        [HttpGet("Todo/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid userId = await Task.Run(YourAction);
            _repository.Remove(id, userId);
            return RedirectToAction("Completed");
        }

        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            Guid userId = await Task.Run(YourAction);
            TodoItem myTodoItem = new TodoItem(model.Text, userId);
            if (ModelState.IsValid)
            {
                _repository.Add(myTodoItem);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        public IActionResult Error()
        {
            return View();
        }

        private async Task<Guid> YourAction()
        {
            // Get currently logged -in user using userManager
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return new Guid(user.Id);
            
        }

    }
}