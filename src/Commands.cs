namespace CodeCrafters.Shell;

public class Commands
{
    public const string EXIT = "exit";
    public const string ECHO = "echo";
    public const string TYPE = "type";
    public const string PWD = "pwd";
    public const string CD = "cd";
    public const string JOBS = "jobs";

    public static IReadOnlyCollection<string> BuiltInCommands => [EXIT, ECHO, TYPE, PWD, CD, JOBS];

    public static string[] GetExecutablePaths()
    {
        var pathString = Environment.GetEnvironmentVariable("PATH");
        return pathString?.Split(Path.PathSeparator, StringSplitOptions.TrimEntries) ?? [];
    }
}