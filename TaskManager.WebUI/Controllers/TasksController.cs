using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Domain.Abstract;
using TaskManager.Domain.Entities;

namespace TaskManager.WebUI.Controllers
{
    public class TasksController : Controller
    {
        private ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public IActionResult List()
        {
            return View(_repository.Tasks);
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            TaskModel task = new TaskModel();
            return PartialView("AddTask", task);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskModel task)
        {
            await _repository.Add(task);
            return RedirectToAction("List");
        }

        public async void CheckedChanged(int id, bool isChecked)
        {
            var task = await _repository.Find(id);
            task.Finished = isChecked;
            await _repository.SaveChanges();
        }

        public async Task<IActionResult> RemoveTask(int id)
        {
            var task = await _repository.Find(id);
            _repository.Remove(task);
            await _repository.SaveChanges();
            return RedirectToAction("List");
        }
    }
}