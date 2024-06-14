namespace TodoList.Commands;

public class UpdateTodoItemCmd
{
  public int TodoItemId { get; set; }
  public required string Content { get; set; }
}
