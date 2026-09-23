namespace CodeCrafters.Shell;

public class Commands
{
    public const string EXIT = "exit";
    public const string ECHO = "echo";
    public const string TYPE = "type";

    public static string[] GetBuiltInCommands()
    {
        return [EXIT, ECHO, TYPE];
    }

    public static string[] GetExecutablePaths()
    {
        var pathString = Environment.GetEnvironmentVariable("PATH");
        return pathString?.Split(Path.PathSeparator, StringSplitOptions.TrimEntries) ?? [];
    }
}