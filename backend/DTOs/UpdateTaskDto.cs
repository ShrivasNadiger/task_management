using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class UpdateTaskDto
{
  [Required]
  public string Title { get; set; } = string.Empty;

  public string? Description { get; set; }

  public string Status { get; set; } = "PENDING";

  public string Priority { get; set; } = "MEDIUM";

  [Required]
  public int UserId { get; set; }
}