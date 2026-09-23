using System.Linq.Expressions;
using CodeCrafters.Shell;

class Program
{
    static void Main()
    {
        while (true)
        {    
            Console.Write("$ ");
            
            var input = Console.ReadLine();
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
