using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.Domain.Abstract;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Entities.Identity;
using TaskManager.WebUI.ViewModels.Tasks;

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

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Receive services through DI
        /// </summary>
        /// <param name="repository">The Tasks table repository</param>
        /// <param name="userManager">Control the user settings</param>
        /// <param name="mapper">AutoMapper</param>
        public TasksController(ITaskRepository repository, UserManager<UserModel> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Main view that shows all of the user's tasks
        /// </summary>
        /// <returns>View with user's tasks</returns>
        [Route("")]
        public async Task<IActionResult> Read()
        {
            int userId = int.Parse(_userManager.GetUserId(User));

            await _userManager.FindByIdAsync(userId.ToString()).ContinueWith(task =>
            {
                ViewBag.Email = task.Result.Email;
            });

            var tasks = _mapper.ProjectTo<ReadViewModel>(_repository.Tasks.Where(x => x.UserId == userId).AsQueryable());
            return View(tasks);
        }

        /// <summary>
        /// GET Create task action 
        /// </summary>
        /// <returns>Partial view that represents a modal window for task creation</returns>
        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("Create", new CreateViewModel());
        }

        /// <summary>
        /// POST Create task action
        /// </summary>
        /// <param name="task">New task</param>
        /// <returns>Redirection to the main view (AJAX)</returns>
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel createTask)
        {
            if (ModelState.IsValid)
            {
                TaskModel task = _mapper.Map<TaskModel>(createTask);
                task.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _repository.CreateAsync(task);

                return Json(new
                {
                    status = "success"
                });
            }

            return Json(new
            {
                status = "failure",
                formErrors = ModelState.Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(e => e.ErrorMessage) })
            });
        }

        /// <summary>
        /// GET Update task action
        /// </summary>
        /// <param name="id">Id of the updated task</param>
        /// <returns>Partial view that represents a modal window for task updating</returns>
        [Route("Update/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var task = await _repository.FindAsync(id);
            
            if (task?.UserId == int.Parse(_userManager.GetUserId(User)))
            {
                UpdateViewModel updateTask = _mapper.Map<UpdateViewModel>(task);
                return PartialView("Update", updateTask);
            }

            return RedirectToAction("Read");
        }

        /// <summary>
        /// POST Update task action
        /// </summary>
        /// <param name="task">Updated task</param>
        /// <returns>Redirection to the main view (AJAX)</returns>
        [Route("Update/{id:int}")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel updateTask)
        {
            if (ModelState.IsValid)
            {
                TaskModel task = _mapper.Map<TaskModel>(updateTask);
                await _repository.UpdateAsync(task);
                return Json(new
                {
                    status = "success"
                });
            }

            return Json(new
            {
                status = "failure",
                formErrors = ModelState.Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(e => e.ErrorMessage) })
            });
        }

        /// <summary>
        /// GET Delete task action
        /// </summary>
        /// <param name="id">Id of the removing task</param>
        /// <returns>Partial view that represents a modal window for task deletion</returns>
        [Route("Delete/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _repository.FindAsync(id);

            if (task?.UserId == int.Parse(_userManager.GetUserId(User)))
            {
                DeleteViewModel deleteTask = _mapper.Map<DeleteViewModel>(task);
                return PartialView("Delete", deleteTask);
            }

            return RedirectToAction("Read");
        }

        /// <summary>
        /// POST Delete task action
        /// </summary>
        /// <param name="task">Task to be removed</param>
        /// <returns>Redirection to the main view</returns>
        [Route("Delete/{id:int}")]
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel deleteTask)
        {
            TaskModel task = _mapper.Map<TaskModel>(deleteTask);
            await _repository.DeleteAsync(task);
            return RedirectToAction("Read");
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