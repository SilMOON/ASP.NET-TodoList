namespace TodoList.Entities;

public class TodoItem
{
  public int TodoItemId { get; set; }
  public int UserId { get; set; }
  public required string Content { get; set; }
}
