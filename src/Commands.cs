namespace CodeCrafters.Shell;

public class Commands
{
    public const string EXIT = "exit";
    public const string EXIT_D = "exitd";

    public const string ECHO = "echo";
    public const string TYPE = "type";
    public const string PWD = "pwd";
    public const string CD = "cd";

    public static string[] GetBuiltInCommands()
    {
        return [EXIT, EXIT_D, ECHO, TYPE, PWD, CD];
    }

    public static string[] GetExecutablePaths()
    {
        var pathString = Environment.GetEnvironmentVariable("PATH");
        return pathString?.Split(Path.PathSeparator, StringSplitOptions.TrimEntries) ?? [];
    }
}