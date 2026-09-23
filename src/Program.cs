class Program
{
    static void Main()
    {
        while (true)
        {    
            Console.Write("$ ");
            var cmd = Console.ReadLine();
            Console.WriteLine($"{cmd}: command not found");
        }
    }
}
