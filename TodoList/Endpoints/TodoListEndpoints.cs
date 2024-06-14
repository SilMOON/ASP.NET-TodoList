using TodoList.Commands;
using TodoList.Services;

namespace TodoList.Endpoints;

public static class TodoListEndpoints
{
  public static void RegisterTodoListEndpoints(this IEndpointRouteBuilder endpoints)
  {
    endpoints.MapGet("/todoitems", async (TodoListService todoListService) =>
        await todoListService.ListTodoItems(1)
      )
      .WithName("todoitems");

    endpoints.MapPost("/todoitem", async (AddTodoItemCmd addTodoItemCmd, TodoListService todoListService) =>
      await todoListService.AddTodoItem(addTodoItemCmd, 1)
      );
  }
}
