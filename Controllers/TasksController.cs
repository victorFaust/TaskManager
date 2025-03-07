using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Models.Dtos;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskContext _context;
        private readonly IMapper _mapper;

        public TasksController(TaskContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: /Tasks
        public IActionResult Index()
        {
            var tasks = _context.Tasks.OrderBy(t => t.Priority).ToList();
            return View(tasks);
        }

        // GET: /Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTaskDto createTaskDto)
        {
            if (ModelState.IsValid)
            {
                var task = _mapper.Map<MyTasks>(createTaskDto);
                _context.Add(task);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Task created successfully!" });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Validation failed.", errors });
        }

        // GET: /Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            var editTaskDto = _mapper.Map<EditTaskDto>(task);
            return View(editTaskDto);
        }

        // POST: /Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTaskDto editTaskDto)
        {
            if (id != editTaskDto.Id) return Json(new { success = false, message = "Invalid task ID." });

            if (ModelState.IsValid)
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null) return Json(new { success = false, message = "Task not found." });

                _mapper.Map(editTaskDto, task);
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Task updated successfully!" });
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    if (!_context.Tasks.Any(e => e.Id == task.Id))
                        return Json(new { success = false, message = "Task no longer exists." });
                    throw;
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Validation failed.", errors });
        }

        // POST: /Tasks/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return Json(new { success = false, message = "Task not found." });

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Task deleted successfully!" });
        }

        // POST: /Tasks/ToggleComplete/5
        [HttpPost]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return Json(new { success = false, message = "Task not found." });

            task.IsCompleted = !task.IsCompleted;
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = task.IsCompleted ? "Task marked as completed!" : "Task marked as incomplete!" });
        }
    }
}