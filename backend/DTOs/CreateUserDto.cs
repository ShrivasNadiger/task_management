using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class CreateUserDto
{
  [Required]
  public string Name { get; set; } = string.Empty;

  [Required]
  [EmailAddress]
  public string Email { get; set; } = string.Empty;
}