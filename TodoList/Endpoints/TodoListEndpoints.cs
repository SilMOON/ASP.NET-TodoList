using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TodoList.Commands;
using TodoList.Services;

namespace TodoList.Endpoints;

public static class TodoListEndpoints
{
  public static void RegisterTodoListEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var routes = endpoints.MapGroup("todoitem")
      .WithParameterValidation()
      .WithOpenApi()
      .WithTags("TodoItem")
      .RequireAuthorization();

    routes.MapGet("/", async (
      TodoListService todoListService,
      ClaimsPrincipal user) =>
        {
          var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
          if (userId == null) return Results.Unauthorized();

          var todoItems = await todoListService.ListTodoItems(userId);
          return Results.Ok(todoItems);
        }
      )
      .WithSummary("List todo items.")
      .ProducesProblem(StatusCodes.Status401Unauthorized);

    routes.MapPost("/", async (
        AddTodoItemCmd addTodoItemCmd,
        TodoListService todoListService,
        ClaimsPrincipal user) =>
      {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Results.Unauthorized();

        var todoItemId = await todoListService.AddTodoItem(addTodoItemCmd, userId);
        return Results.Ok(todoItemId);
      }
    );

    routes.MapPut("/", async (
        UpdateTodoItemCmd updateTodoItemCmd,
        TodoListService todoListService,
        ClaimsPrincipal user) =>
      {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Results.Unauthorized();

        var todoItemId = await todoListService.UpdateTodoItem(updateTodoItemCmd, userId);
        return Results.Ok(todoItemId);
      }
    );

    routes.MapDelete("/{id}", async (
        string id,
        TodoListService todoListService,
        ClaimsPrincipal user) =>
      {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Results.Unauthorized();

        await todoListService.DeleteTodoItem(int.Parse(id), userId);
        return Results.NoContent();
      }
    );
  }
}
