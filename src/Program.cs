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

            var cmd = tokens?.Length > 0 ? tokens[0] : input;
            var args = tokens?.Length > 1 ? tokens[1..] : [string.Empty]; 

            switch (cmd)
            {
                case Commands.EXIT:
                    Environment.Exit(0);
                    break;
                case Commands.ECHO:
                    var output = string.Join(' ', args);
                    Console.WriteLine(output);
                    break;
                case Commands.TYPE:
                    var builtInCommands = Commands.GetBuiltInCommands();
                    var argumentCmd = args.Length > 0 ? args[0] : string.Empty;

                    if (builtInCommands.Contains(argumentCmd))
                    {
                        Console.WriteLine($"{argumentCmd} is a shell {CommandTypes.BUILT_IN}");
                    }
                    else
                    {
                        Console.WriteLine($"{argumentCmd}: not found");
                    }

                    break;
                default:
                    Console.WriteLine($"{cmd}: command not found");
                    break;
            }
        }
    }
}
