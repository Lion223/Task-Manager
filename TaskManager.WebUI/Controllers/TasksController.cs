using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Domain.Abstract;
using TaskManager.Domain.Entities;

namespace TaskManager.WebUI.Controllers
{
    public class TasksController : Controller
    {
        #region Private members

        /// <summary>
        /// The Tasks table repository
        /// </summary>
        private readonly ITaskRepository _repository;

        #endregion

        #region Constructor

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        #endregion

        [Route("")]
        public IActionResult List()
        {
            return View(_repository.Tasks);
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            TaskModel task = new TaskModel();
            return PartialView("CreateTask", task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskModel task)
        {
            await _repository.Create(task);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTask(int id)
        {
            var task = await _repository.Find(id);
            return PartialView("UpdateTask", task);
        }

        [HttpPost]
        public IActionResult UpdateTask(TaskModel task)
        {
            _repository.Update(task);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _repository.Find(id);
            _repository.Delete(task);
            return RedirectToAction("List");
        }

        public async void CheckedChanged(int id, bool isChecked)
        {
            var task = await _repository.Find(id);
            task.Finished = isChecked;
            await _repository.SaveChanges();
        }
    }
}