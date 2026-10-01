using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly AppDbContext _context;

  public UsersController(AppDbContext context)
  {
    _context = context;
  }

  // GET: /api/users
  [HttpGet]
  public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
  {
    var users = await _context.Users
        .Select(user => new UserResponseDto
        {
          Id = user.Id,
          Name = user.Name,
          Email = user.Email,
          CreatedAt = user.CreatedAt
        })
        .ToListAsync();

    return Ok(users);
  }

  // GET: /api/users/1
  [HttpGet("{id}")]
  public async Task<ActionResult<UserResponseDto>> GetUser(int id)
  {
    var user = await _context.Users
        .Where(user => user.Id == id)
        .Select(user => new UserResponseDto
        {
          Id = user.Id,
          Name = user.Name,
          Email = user.Email,
          CreatedAt = user.CreatedAt
        })
        .FirstOrDefaultAsync();

    if (user == null)
    {
      return NotFound();
    }

    return Ok(user);
  }

  // POST: /api/users
  [HttpPost]
  public async Task<ActionResult<UserResponseDto>> CreateUser(
      CreateUserDto dto)
  {
    var user = new User
    {
      Name = dto.Name,
      Email = dto.Email,
      CreatedAt = DateTime.UtcNow
    };

    _context.Users.Add(user);

    await _context.SaveChangesAsync();

    var response = new UserResponseDto
    {
      Id = user.Id,
      Name = user.Name,
      Email = user.Email,
      CreatedAt = user.CreatedAt
    };

    return CreatedAtAction(
        nameof(GetUser),
        new { id = user.Id },
        response
    );
  }

  // PUT: /api/users/1
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUser(
      int id,
      UpdateUserDto dto)
  {
    var user = await _context.Users.FindAsync(id);

    if (user == null)
    {
      return NotFound();
    }

    user.Name = dto.Name;
    user.Email = dto.Email;

    await _context.SaveChangesAsync();

    return NoContent();
  }

  // DELETE: /api/users/1
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(int id)
  {
    var user = await _context.Users.FindAsync(id);

    if (user == null)
    {
      return NotFound();
    }

    _context.Users.Remove(user);

    await _context.SaveChangesAsync();

    return NoContent();
  }

  [HttpGet("{id}/tasks")]
  public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetUserTasks(int id)
  {
    var userExists = await _context.Users
        .AnyAsync(user => user.Id == id);

    if (!userExists)
    {
      return NotFound("User does not exist.");
    }

    var tasks = await _context.Tasks
        .Where(task => task.UserId == id)
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
}