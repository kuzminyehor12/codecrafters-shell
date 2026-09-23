class Program
{
    static void Main()
    {
        Console.Write("$ ");
        var cmd = Console.ReadLine();
        Console.WriteLine($"{cmd}: command not found");
    }
}
