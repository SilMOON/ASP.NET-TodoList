using Microsoft.EntityFrameworkCore;
using TodoList.Commands;
using TodoList.Entities;
using TodoList.Repositories;

namespace TodoList.Services;

public class TodoListService(AppDbContext context)
{
  public async Task<int> AddTodoItem(AddTodoItemCmd cmd, int userId)
  {
    TodoItem todoItem = new TodoItem
    {
      UserId = userId,
      Content = cmd.Content
    };
    context.Add(todoItem);
    await context.SaveChangesAsync();
    return todoItem.TodoItemId;
  }

  public async Task<ICollection<TodoItem>> ListTodoItems(int userId)
  {
    return await context.TodoItems
      .Where(t => t.UserId == userId)
      .ToListAsync();
  }

  public async Task<int> UpdateTodoItem(UpdateTodoItemCmd cmd, int userId)
  {
    TodoItem todoItem = await context.TodoItems
      .FindAsync(cmd.TodoItemId) ?? throw new Exception("Unable to find the todo item.");

    if (todoItem.UserId != userId)
    {
      throw new Exception("Permission denied.");
    }

    todoItem.Content = cmd.Content;
    await context.SaveChangesAsync();

    return todoItem.TodoItemId;
  }

  public async Task DeleteTodoItem(int todoItemId, int userId)
  {
    TodoItem todoItem = await context.TodoItems
      .FindAsync(todoItemId) ?? throw new Exception("Unable to find the todo item.");

    if (todoItem.UserId != userId)
    {
      throw new Exception("Permission denied.");
    }

    context.Remove(todoItem);
  }
}
