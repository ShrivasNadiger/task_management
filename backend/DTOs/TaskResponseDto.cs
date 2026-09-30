namespace backend.DTOs;

public class TaskResponseDto
{
  public int Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string? Description { get; set; }

  public string Status { get; set; } = string.Empty;

  public string Priority { get; set; } = string.Empty;

  public int UserId { get; set; }

  public string? UserName { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }
}