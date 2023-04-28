using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAppBackend.Data;
using ToDoAppBackend.Dtos;
using ToDoAppBackend.Models;

namespace ToDoAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            return await _context.TaskItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return taskItem;
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItemCreateDto item)
        {
            var taskItem = new TaskItem
            {
                Title = item.Title,
                Description = item.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTodoItem(int id, TaskItemUpdateDto item)
        {
            var taskItemSaved = await _context.TaskItems.FirstOrDefaultAsync(task => task.Id == id);

            if (taskItemSaved == null)
            {
                return NotFound();
            }

            // Update the entity using values from the DTO
            taskItemSaved.Title = item.Title ?? taskItemSaved.Title;
            taskItemSaved.Description = item.Description ?? taskItemSaved.Description;
            taskItemSaved.UpdatedAt = DateTime.Now;

            _context.Entry(taskItemSaved).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}