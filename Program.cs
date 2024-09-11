class Program
{
  public static async System.Threading.Tasks.Task Main()
  {    
    Cli taskTrackerCli = new Cli();
    taskTrackerCli.LoadTasks();
    await taskTrackerCli.StartCli();
  }
}