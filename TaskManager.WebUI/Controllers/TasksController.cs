using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Domain.Abstract;
using TaskManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TaskManager.Domain.Entities.Identity;
using System.Security.Claims;
using System.Linq;

namespace TaskManager.WebUI.Controllers
{
    /// <summary>
    /// Tasks controller that holds all logic necessary to work with tasks
    /// </summary>
    [Authorize]
    public class TasksController : Controller
    {
        #region Private members

        /// <summary>
        /// The Tasks table repository
        /// </summary>
        private readonly ITaskRepository _repository;

        /// <summary>
        /// Control the user settings
        /// </summary>
        private readonly UserManager<UserModel> _userManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Receive Task repository and User manager through DI
        /// </summary>
        /// <param name="repository"></param>
        public TasksController(ITaskRepository repository, UserManager<UserModel> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Main view that shows all of the user's tasks
        /// </summary>
        /// <returns>View with user's tasks</returns>
        [Route("")]
        public async Task<IActionResult> List()
        {
            int userId = int.Parse(_userManager.GetUserId(User));
            await _userManager.FindByIdAsync(userId.ToString()).ContinueWith(task =>
            {
                ViewBag.Email = task.Result.Email;
            });
            return View(_repository.Tasks.Where(x => x.UserId == userId));
        }

        /// <summary>
        /// GET Create task action 
        /// </summary>
        /// <returns>Partial view that represents a modal window for task creation</returns>
        [Route("Create")]
        [HttpGet]
        public IActionResult CreateTask()
        {
            TaskModel task = new TaskModel { UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) };
            return PartialView("CreateTask", task);
        }

        /// <summary>
        /// POST Create task action
        /// </summary>
        /// <param name="task">New task</param>
        /// <returns>Redirection to the main view</returns>
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskModel task)
        {
            await _repository.CreateAsync(task);
            return RedirectToAction("List");
        }

        /// <summary>
        /// GET Update task action
        /// </summary>
        /// <param name="id">Id of the updated task</param>
        /// <returns>Partial view that represents a modal window for task updating</returns>
        [Route("Update/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> UpdateTask(int id)
        {
            var task = await _repository.FindAsync(id);
            
            if (task?.UserId == int.Parse(_userManager.GetUserId(User)))
            {
                return PartialView("UpdateTask", task);
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// POST Update task action
        /// </summary>
        /// <param name="task">Updated task</param>
        /// <returns>Redirection to the main view</returns>
        [Route("Update/{id:int}")]
        [HttpPost]
        public async Task<IActionResult> UpdateTask(TaskModel task)
        {
            await _repository.UpdateAsync(task);
            return RedirectToAction("List");
        }

        /// <summary>
        /// GET Delete task action
        /// </summary>
        /// <param name="id">Id of the removing task</param>
        /// <returns>Partial view that represents a modal window for task deletion</returns>
        [Route("Delete/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _repository.FindAsync(id);

            if (task?.UserId == int.Parse(_userManager.GetUserId(User)))
            {
                return PartialView("DeleteTask", task);
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// POST Delete task action
        /// </summary>
        /// <param name="id">Id of the removing task</param>
        /// <returns>Redirection to the main view</returns>
        [Route("Delete/{id:int}")]
        [HttpPost, ActionName("DeleteTask")]
        public async Task<IActionResult> DeleteTaskPost(int id)
        {
            var task = await _repository.FindAsync(id);
            await _repository.DeleteAsync(task);
            return RedirectToAction("List");
        }

        /// <summary>
        /// GET Finished changed action
        /// </summary>
        /// <param name="id">Id of the changed task</param>
        /// <param name="isChecked">Boolean that indicates whether the task is finished or not</param>
        /// <returns></returns>
        [Route("Finished/{id:int}/{isChecked:bool}")]
        public async Task CheckedChanged(int id, bool isChecked)
        {
            var task = await _repository.FindAsync(id);

            if (task?.UserId == int.Parse(_userManager.GetUserId(User)))
            {
                task.Finished = isChecked;
                await _repository.SaveChangesAsync();
            }
        }

        #endregion
    }
}