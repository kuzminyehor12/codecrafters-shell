using CodeCrafters.Shell;
using ReadLineExt = ReadLine.ReadLine;

class Program
{
    static void Main()
    {
        const string prompt = "$ ";
        ReadLineExt.Context.AutoCompletionHandler = new AutocompletionHandler(prompt);

        while (true)
        {          
            var input = ReadLineExt.Read(prompt);
            var tokens = input?.Split(' ');

            var cmdToken = tokens?.Length > 0 ? tokens[0] : input;
            var args = tokens?.Length > 1 ? tokens[1..] : [string.Empty]; 

            var cmd = CommandResolver.Resolve(cmdToken);

            if (cmd is not null)
            {
                cmd.Execute(args);
            }
            else
            {
                Console.WriteLine($"{cmdToken}: command not found");
            }
        }
    }
}
