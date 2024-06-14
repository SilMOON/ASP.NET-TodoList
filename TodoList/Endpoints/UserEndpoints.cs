using TodoList.Commands;
using TodoList.Services;

namespace TodoList.Endpoints;

public static class UserEndpoints
{
  public static void RegisterUserEndpoints(this IEndpointRouteBuilder endpoints)
  {
    endpoints.MapPost("/user", async (AddUserCmd addUserCmd, UserService userService) =>
      await userService.AddUser(addUserCmd)
    );
  }
}
