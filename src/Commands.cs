namespace CodeCrafters.Shell;

public class Commands
{
    public const string EXIT = "exit";
    public const string ECHO = "echo";
    public const string TYPE = "type";
    public const string PWD = "pwd";
    public const string CD = "cd";

    public static IReadOnlyCollection<string> BuiltInCommands => [EXIT, ECHO, TYPE, PWD, CD];

    private static string[] _executablePaths = [];

    public static IReadOnlyCollection<string> ExecutablePaths
    {
        get => _executablePaths;
        set
        {
            var pathString = Environment.GetEnvironmentVariable("PATH");
            _executablePaths = pathString?.Split(Path.PathSeparator, StringSplitOptions.TrimEntries) ?? [];
        }
    }
}