using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

public class Cli
{
  public async Task<int> StartCli() =>
    await new CliApplicationBuilder()
      .AddCommandsFromThisAssembly()
      .SetExecutableName("task-tracker")
      .SetTitle("Task Tracker")
      .SetDescription("A task tracking application")
      .Build()
      .RunAsync();
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