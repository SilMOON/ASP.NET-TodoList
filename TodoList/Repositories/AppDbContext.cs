using Microsoft.EntityFrameworkCore;
using TodoList.Entities;

namespace TodoList.Repositories;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  public DbSet<User> Users { get; set; }
  public DbSet<TodoItem> TodoItems { get; set; }
}
