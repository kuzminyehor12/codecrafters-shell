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

        var suggestedBuiltIns = Commands.GetBuiltInCommands();
        var suggestedExecutables = GetExecutables();

        string[] suggestedCommands = suggestedBuiltIns
            .Union(suggestedExecutables)
            .Where(cmd => !string.IsNullOrWhiteSpace(cmd) && cmd.StartsWith(text))
            .Select(cmd => cmd + " ")
            .ToArray();

        return suggestedCommands.Length > 0 ? suggestedCommands : [text + "\a"];
    }

    private IEnumerable<string> GetExecutables()
    {
        var executablePaths = Commands.GetExecutablePaths();
        return executablePaths
            .Where(Directory.Exists)
            .SelectMany(Directory.EnumerateFiles)
            .Where(FileUtilities.IsExecutable)
            .Select(Path.GetFileNameWithoutExtension);
    }
}