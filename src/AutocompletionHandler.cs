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
        var suggestedCommands = builtInCommands.Where(cmd => cmd.StartsWith(text)).Select(cmd => cmd + " ").ToArray();

        return suggestedCommands.Length > 0 ? suggestedCommands : ["\u0007"];
    }
}