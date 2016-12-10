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
            var resultTask = Task.Run(YourAction);
            ApplicationUser currentUser = await resultTask;
            var todos = _repository.GetActive(new Guid(currentUser.Id));
            return View(todos);
        }
        /*
        [HttpPost]
        public async Task<IActionResult> Index(Guid Id)
        {
            var resultTask = Task.Run(YourAction);
            ApplicationUser currentUser = await resultTask;
            _repository.MarkAsCompleted(Id, new Guid(currentUser.Id));
            return View();
        }
        */
        public async Task<IActionResult> Completed()
        {
            var resultTask = Task.Run(YourAction);
            ApplicationUser currentUser = await resultTask;
            var todos = _repository.GetCompleted(new Guid(currentUser.Id));
            return View(todos);
        }

        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            var resultTask = Task.Run(YourAction);
            ApplicationUser currentUser = await resultTask;
            TodoItem myTodoItem = new TodoItem(model.Text, new Guid(currentUser.Id));
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

        private async Task<ApplicationUser> YourAction()
        {
            // Get currently logged -in user using userManager
            return await _userManager.GetUserAsync(HttpContext.User);
            
        }

    }
}