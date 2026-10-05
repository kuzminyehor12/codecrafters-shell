using System.Net.NetworkInformation;
using ReadLine;

namespace CodeCrafters.Shell;

public class AutocompletionHandler : IAutoCompleteHandler
{
    private readonly string _prompt;

    public AutocompletionHandler(string prompt)
    {
        _prompt = prompt;
    }

    public char[] Separators { get; set; } = [];

    public string[] GetSuggestions(string text, int index)
    {
        if (text.Length < 3)
        {
            return null;
        }

        var builtIns = Commands.BuiltInCommands;
        var executables = GetExecutables();

        string[] suggestedCommands = builtIns
            .Union(executables)
            .Where(cmd => !string.IsNullOrWhiteSpace(cmd) && cmd.StartsWith(text))
            .OrderBy(cmd => cmd)
            .ToArray();

        var multipleSuggestions = suggestedCommands.Length > 1;
        
        if (multipleSuggestions)
        {
            var hasUnderscore = text[index] == '_';
            string matchedPrefix = string.Empty;

            if (hasUnderscore)
            {
                var substringTillNextUnderscore = (string sub) =>
                {
                    var subTillUnderscore = sub.TakeWhile(ch => ch == '_');
                    return subTillUnderscore.Count();
                };

                matchedPrefix = suggestedCommands
                    .Select(c => c.Substring(index + 1, substringTillNextUnderscore(c.Substring(index + 1))))
                    .Where(prefix => !string.IsNullOrWhiteSpace(prefix))
                    .GroupBy(prefix => prefix, (key, group) => new { Prefix = key, Count = group.Count() })
                    .OrderByDescending(prefixGroup => prefixGroup.Count)
                    .FirstOrDefault()?.Prefix ?? string.Empty;
            }

            var suggestionResult = !string.IsNullOrEmpty(matchedPrefix) ? [text + matchedPrefix] : AlarmText(text);
            return [..suggestionResult, NextAutocompletionWithPrint(text, suggestedCommands)];
        }

        return suggestedCommands.Length > 0 ? [$"{suggestedCommands[0]} "] : AlarmText(text);
    }

    private string NextAutocompletionWithPrint(string text, string[] suggestedCommands)
    {
        var commandsAsString = string.Join(" ", suggestedCommands);
        var prompt = _prompt + text;
        return string.Join('\n', $" {text}", commandsAsString, prompt);
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