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

        var builtInCommands = Commands.GetBuiltInCommands();
        return builtInCommands.Where(cmd => cmd.StartsWith(text)).ToArray();
    }
}