using CodeCrafters.Shell;

class Program
{
    static void Main()
    {
        while (true)
        {    
            Console.Write("$ ");
            var cmd = Console.ReadLine();

            switch (cmd)
            {
                case Commands.EXIT:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"{cmd}: command not found");
                    break;
            }
        }
    }
}
