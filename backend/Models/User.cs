namespace backend.Models;

public class User
{
  public int Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public string? PasswordHash { get; set; }

  public string Role { get; set; } = "User";

  public DateTime CreatedAt { get; set; }

  public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}