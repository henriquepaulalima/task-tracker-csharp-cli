using System.Text.Json;

public class Task
{
  public int id;
  public string description = "";
  public TaskStatus status;
  public DateTime createdAt = new DateTime();
  public DateTime updatedAt = new DateTime();

  public Task(int _id, string _description)
  {
    id = _id;
    description = _description;
    status = TaskStatus.ToDo;
  }
}

public enum TaskStatus {
  ToDo,
  InProgress,
  Done
}