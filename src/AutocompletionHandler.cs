using ReadLine;

namespace CodeCrafters.Shell;

public class AutocompletionHandler : IAutoCompleteHandler
{
    public char[] Separators { get; set; } = [];

    public string[] GetSuggestions(string text, int index)
    {
        if (text.Length < 3)
        {
            return [];
        }

        var suggestedBuiltIns = FindBuiltInCommands(text);
        var suggestedExecutables = FindExecutables(text);
        string[] suggestedCommands = [..suggestedBuiltIns, ..suggestedExecutables];
        return suggestedCommands.Length > 0 ? suggestedCommands : [text + "\a"];
    }

    private string[] FindExecutables(string text)
    {
        var executablePaths = Commands.GetExecutablePaths();
        return executablePaths
            .Where(Directory.Exists)
            .SelectMany(Directory.EnumerateFiles)
            .Where(fileName => Path.GetExtension(fileName) == "exe")
            .Select(Path.GetFileNameWithoutExtension)
            .Where(exe => !string.IsNullOrWhiteSpace(exe) && exe.StartsWith(text))
            .ToArray();
    }

    private string[] FindBuiltInCommands(string text)
    {
        var builtInCommands = Commands.GetBuiltInCommands();
        return builtInCommands
            .Where(cmd => cmd.StartsWith(text))
            .Select(cmd => cmd + " ")
            .ToArray();
    }
}