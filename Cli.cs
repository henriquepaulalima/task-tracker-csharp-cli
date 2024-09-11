using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

public class Cli
{
  public readonly string dirPath = Environment.CurrentDirectory;
  public readonly string fileName = "tasks.json";
  public string? tasks;

  public async Task<int> StartCli()
  {
    return await new CliApplicationBuilder()
      .AddCommandsFromThisAssembly()
      .SetExecutableName("task-tracker")
      .SetTitle("Task Tracker")
      .SetDescription("A task tracking application")
      .Build()
      .RunAsync();
  }

  public void LoadTasks()
  {
    string filePath = $"{dirPath}/{fileName}";

    try
    {
      tasks = LoadFile(filePath);

      if (tasks == null || tasks == "")
      {
        Console.Write("Tasks file not found, create a new empty file? (y/n) ");
        string input = Console.ReadLine() ?? "";

        if (input != "" && input != null && input.ToLower() == "y")
        {
          tasks = CreateFile(filePath);
        }
        else
        {
          Console.WriteLine("Program is ended due to no tasks file.");
        }
      }
    }
    catch (IOException error)
    {
      Console.Error.WriteLine(error);
      throw;
    }
  }

  private string LoadFile(string filePath)
  {
    try
    {
      FileStream fs = File.Open(filePath, FileMode.Open);
      fs.Close();
      tasks = File.ReadAllText(filePath);
      return tasks;
    }
    catch (IOException)
    {
      return "";
    }
  }

  private string CreateFile(string filePath)
  {
    if (File.Exists(filePath))
    {
      File.Delete(filePath);
    }

    File.Create(filePath).Dispose();
    FileStream fs = File.Open(filePath, FileMode.Open);
    fs.Close();
    File.WriteAllText(filePath, "{\"tasks\":[]}");

    string tasks = File.ReadAllText(filePath);
    return tasks;
  }
}

[Command("add")]
public class AddCommand : ICommand
{
  [CommandParameter(0)]
  public int TaskId { get; set; }
  [CommandParameter(1)]
  public string TaskDescription { get; set; } = "";

  public ValueTask ExecuteAsync(IConsole console)
  {
    Task newTask = new Task(TaskId, TaskDescription);
    console.Output.WriteLine($"New task created: id: {newTask.id}, description: {newTask.description}");

    return default;
  }
}