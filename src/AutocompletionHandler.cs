using ReadLine;

namespace CodeCrafters.Shell;

public class AutocompletionHandler : IAutoCompleteHandler
{
    private readonly TextWriter _writer;
    private bool _multipleSuggestionsSecondRun = false;

    public AutocompletionHandler(TextWriter writer)
    {
        _writer = writer;
    }

    public char[] Separators { get; set; } = [];

    public string[] GetSuggestions(string text, int index)
    {
        if (text.Length < 3)
        {
            return null;
        }

        var builtIns = Commands.GetBuiltInCommands();
        var executables = GetExecutables();

        string[] suggestedCommands = builtIns
            .Union(executables)
            .Where(cmd => !string.IsNullOrWhiteSpace(cmd) && cmd.StartsWith(text))
            .OrderBy(cmd => cmd)
            .Select(cmd => cmd + " ")
            .ToArray();
            
        if (suggestedCommands.Length > 1)
        {
            if (!_multipleSuggestionsSecondRun)
            {
                _multipleSuggestionsSecondRun = true;
                return [..AlarmText(text), string.Join(" ", suggestedCommands)];
            }
            else
            {
                _multipleSuggestionsSecondRun = false;
                _writer.WriteLine(string.Join(" ", suggestedCommands));
                return null;
            }
        }

        return suggestedCommands.Length > 0 ? suggestedCommands : AlarmText(text);
    }

    private static string[] AlarmText(string text) => [text + "\a"];

    private static IEnumerable<string> GetExecutables()
    {
        var executablePaths = Commands.GetExecutablePaths();
        return executablePaths
            .Where(Directory.Exists)
            .SelectMany(Directory.EnumerateFiles)
            .Where(FileUtilities.IsExecutable)
            .Select(Path.GetFileNameWithoutExtension);
    }
}