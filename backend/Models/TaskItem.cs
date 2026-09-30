namespace backend.Models;

public class TaskItem
{
  public int Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string? Description { get; set; }

  public string Status { get; set; } = "PENDING";

  public string Priority { get; set; } = "MEDIUM";

  public int UserId { get; set; }

  public User? User { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }
}