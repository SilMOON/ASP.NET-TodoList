
using TodoList.Commands;
using TodoList.Entities;
using TodoList.Repositories;

namespace TodoList.Services;

public class UserService(AppDbContext context)
{
  public async Task<int> AddUser(AddUserCmd cmd)
  {
    User user = new User
    {
      Name = cmd.Name
    };
    context.Add(user);
    await context.SaveChangesAsync();
    return user.UserId;
  }
}
