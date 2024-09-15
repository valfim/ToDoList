using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Data;
using ToDoListAPI.Models;

namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasks()
        {
            try
            {
                var tasks = await _context.TodoTasks.ToListAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetTask(Guid id)
        {
            try
            {
                var task = await _context.TodoTasks.FindAsync(id);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TodoTask>> CreateTask(TodoTask task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("Task object is null");
                }

                task.Id = Guid.NewGuid();
                task.CreatedAt = DateTime.UtcNow; // Use UTC for consistency
                
                _context.TodoTasks.Add(task);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, TodoTask task)
        {
            try
            {
                if (id != task.Id)
                {
                    return BadRequest("Task ID mismatch");
                }

                _context.Entry(task).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    // Log the exception
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                var task = await _context.TodoTasks.FindAsync(id);
                if (task == null)
                {
                    return NotFound();
                }

                _context.TodoTasks.Remove(task);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool TaskExists(Guid id)
        {
            return _context.TodoTasks.Any(e => e.Id == id);
        }
    }
}



