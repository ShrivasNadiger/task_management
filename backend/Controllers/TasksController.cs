using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
  private readonly AppDbContext _context;

  public TasksController(AppDbContext context)
  {
    _context = context;
  }

  // GET: /api/tasks
  [HttpGet]
  public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasks()
  {
    var tasks = await _context.Tasks
        .Include(task => task.User)
        .Select(task => new TaskResponseDto
        {
          Id = task.Id,
          Title = task.Title,
          Description = task.Description,
          Status = task.Status,
          Priority = task.Priority,
          UserId = task.UserId,
          UserName = task.User != null
                ? task.User.Name
                : null,
          CreatedAt = task.CreatedAt,
          UpdatedAt = task.UpdatedAt
        })
        .ToListAsync();

    return Ok(tasks);
  }

  // GET: /api/tasks/1
  [HttpGet("{id}")]
  public async Task<ActionResult<TaskResponseDto>> GetTask(int id)
  {
    var task = await _context.Tasks
        .Include(task => task.User)
        .Where(task => task.Id == id)
        .Select(task => new TaskResponseDto
        {
          Id = task.Id,
          Title = task.Title,
          Description = task.Description,
          Status = task.Status,
          Priority = task.Priority,
          UserId = task.UserId,
          UserName = task.User != null
                ? task.User.Name
                : null,
          CreatedAt = task.CreatedAt,
          UpdatedAt = task.UpdatedAt
        })
        .FirstOrDefaultAsync();

    if (task == null)
    {
      return NotFound();
    }

    return Ok(task);
  }

  // POST: /api/tasks
  [HttpPost]
  public async Task<ActionResult<TaskResponseDto>> CreateTask(
      CreateTaskDto dto)
  {
    var userExists = await _context.Users
        .AnyAsync(user => user.Id == dto.UserId);

    if (!userExists)
    {
      return BadRequest("User does not exist.");
    }

    var task = new TaskItem
    {
      Title = dto.Title,
      Description = dto.Description,
      Status = dto.Status,
      Priority = dto.Priority,
      UserId = dto.UserId,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    };

    _context.Tasks.Add(task);

    await _context.SaveChangesAsync();

    var response = await _context.Tasks
        .Include(t => t.User)
        .Where(t => t.Id == task.Id)
        .Select(t => new TaskResponseDto
        {
          Id = t.Id,
          Title = t.Title,
          Description = t.Description,
          Status = t.Status,
          Priority = t.Priority,
          UserId = t.UserId,
          UserName = t.User != null
                ? t.User.Name
                : null,
          CreatedAt = t.CreatedAt,
          UpdatedAt = t.UpdatedAt
        })
        .FirstAsync();

    return CreatedAtAction(
        nameof(GetTask),
        new { id = task.Id },
        response
    );
  }

  // PUT: /api/tasks/1
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateTask(
      int id,
      UpdateTaskDto dto)
  {
    var task = await _context.Tasks.FindAsync(id);

    if (task == null)
    {
      return NotFound();
    }

    var userExists = await _context.Users
        .AnyAsync(user => user.Id == dto.UserId);

    if (!userExists)
    {
      return BadRequest("User does not exist.");
    }

    task.Title = dto.Title;
    task.Description = dto.Description;
    task.Status = dto.Status;
    task.Priority = dto.Priority;
    task.UserId = dto.UserId;
    task.UpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return NoContent();
  }

  // DELETE: /api/tasks/1
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteTask(int id)
  {
    var task = await _context.Tasks.FindAsync(id);

    if (task == null)
    {
      return NotFound();
    }

    _context.Tasks.Remove(task);

    await _context.SaveChangesAsync();

    return NoContent();
  }
}