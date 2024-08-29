

class Program
{
  public static async System.Threading.Tasks.Task Main(string[] args)
  {    
    Cli myCli = new Cli();
    await myCli.StartCli();
  }
}