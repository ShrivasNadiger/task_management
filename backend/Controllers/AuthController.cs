using backend.Data;
using backend.DTOs.Auth;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly IPasswordHasher<User> _passwordHasher;
  private readonly JwtService _jwtService;

  public AuthController(
        AppDbContext context,
        IPasswordHasher<User> passwordHasher,
        JwtService jwtService
        )
  {
    _context = context;
    _passwordHasher = passwordHasher;
    _jwtService = jwtService;
  }

  [HttpPost("register")]
  public async Task<ActionResult<RegisterResponseDto>> Register(RegisterDTO dto)
  {
    var emailExists = await _context.Users.AnyAsync(user => user.Email == dto.Email);

    if (emailExists)
    {
      return Conflict("Email Already Registered Please Login");
    }

    var user = new User
    {
      Name = dto.Name,
      Email = dto.Email,
      Role = "User",
      CreatedAt = DateTime.UtcNow
    };

    user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

    _context.Users.Add(user);

    await _context.SaveChangesAsync();

    var response = new RegisterResponseDto
    {
      Id = user.Id,
      Name = user.Name,
      Email = user.Email,
      Role = user.Role,
      CreatedAt = user.CreatedAt
    };

    return StatusCode(
    StatusCodes.Status201Created,
    response);
  }

  [HttpPost("login")]
  public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto)
  {
    var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);
    if (user == null)
    {
      return Unauthorized("Invalid Password or Email.");
    }
    if (string.IsNullOrEmpty(user.PasswordHash))
    {
      return Unauthorized("Invalid Email or Password.");
    }

    var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

    if (result == PasswordVerificationResult.Failed)
    {
      return Unauthorized("Invalid Password and User");
    }
    var token = _jwtService.GenerateToken(user);

    var response = new LoginResponseDto
    {
      Id = user.Id,
      Name = user.Name,
      Email = user.Email,
      Role = user.Role,
      Token = token
    };

    return Ok(response);
  }

}