using ReadLine;

namespace CodeCrafters.Shell;

public class AutocompletionHandler : IAutoCompleteHandler
{
    private readonly TextWriter _writer;
    private bool _multipleSuggestionsCheckedOnce = false;

    public AutocompletionHandler(TextWriter writer)
    {
        _writer = writer;
    }

    public char[] Separators { get; set; } = [];

    public string[] GetSuggestions(string text, int index)
    {
        if (text.Length < 3)
        {
            return [];
        }

        var builtIns = Commands.GetBuiltInCommands();
        var executables = GetExecutables();

        string[] suggestedCommands = builtIns
            .Union(executables)
            .Where(cmd => !string.IsNullOrWhiteSpace(cmd) && cmd.StartsWith(text))
            .Select(cmd => cmd + " ")
            .OrderBy(c => c)
            .ToArray();
            
        if (suggestedCommands.Length > 1)
        {
            if (!_multipleSuggestionsCheckedOnce)
            {
                _multipleSuggestionsCheckedOnce = true;
                return AlarmText(text);
            }
            else
            {
                try
                {
                    _multipleSuggestionsCheckedOnce = false;
                    return [text];
                }
                finally
                {
                    _writer.WriteLine(string.Join(" ", suggestedCommands));
                }
            }
        }

        return suggestedCommands.Length > 0 ? suggestedCommands : AlarmText(text);
    }

    private static string[] AlarmText(string text) => [text + "\a"];

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