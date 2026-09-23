namespace CodeCrafters.Shell;

public class Commands
{
    public const string EXIT = "exit";
    public const string ECHO = "echo";
    public const string TYPE = "type";

    public static IEnumerable<string> GetBuiltInCommands()
    {
        yield return EXIT;
        yield return ECHO;
        yield return TYPE;
    }
}