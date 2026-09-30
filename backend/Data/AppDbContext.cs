using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
  {
  }

  public DbSet<User> Users => Set<User>();

  public DbSet<TaskItem> Tasks => Set<TaskItem>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

    modelBuilder.Entity<TaskItem>()
        .HasOne(t => t.User)
        .WithMany(u => u.Tasks)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}