using ReadLine;

namespace CodeCrafters.Shell;

public class AutocompletionHandler : IAutoCompleteHandler
{
    public char[] Separators { get; set; } = [];

    public string[] GetSuggestions(string text, int index)
    {
        if (text.Length < 3)
        {
            return null;
        }

        var builtInCommands = Commands.GetBuiltInCommands();

        return builtInCommands.Aggregate(builtInCommands, (suggestedCommands, cmd) =>
        {
            if (!cmd.StartsWith(text))
            {
                suggestedCommands[suggestedCommands.IndexOf(cmd)] = null;
            }

            return suggestedCommands;
        });
    }
}