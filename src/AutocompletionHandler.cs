using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using ReadLine;

namespace CodeCrafters.Shell;

public class AutocompletionHandler : IAutoCompleteHandler
{
    private readonly string _prompt;

    public AutocompletionHandler(string prompt)
    {
        _prompt = prompt;
    }

    public char[] Separators { get; set; } = ['_'];

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

        int CountTillNextSeparator(string sub)
        {
            var subTillSeparator = sub.TakeWhile(ch => !Separators.Contains(ch));
            return subTillSeparator.Count();
        }

        var multipleSuggestions = suggestedCommands.Length > 1;

        var matchedPrefixGroup = multipleSuggestions 
            ? suggestedCommands
                .Where(c => c.Length >= index + 1)
                .Select(c => c.Substring(index, CountTillNextSeparator(c.Substring(index))))
                .GroupBy(prefix => prefix, (key, group) => new { Prefix = key, Count = group.Count() })
                .OrderByDescending(prefixGroup => prefixGroup.Count)
                .FirstOrDefault()
            : new { Prefix = suggestedCommands[0], Count = 1};
        
        var suggestionResult = matchedPrefixGroup is not null 
            && (!(multipleSuggestions && matchedPrefixGroup.Count == 1) || !multipleSuggestions)
                ? [matchedPrefixGroup.Prefix] 
                : new string[] { "\a" };

        if (multipleSuggestions)
        {
            return [..suggestionResult, NextAutocompletionWithPrint(text, index, suggestedCommands)];
        }

        return suggestedCommands.Length > 0 ? [$"{suggestionResult[0]} "] : AlarmText(text);
    }

    private string NextAutocompletionWithPrint(string text, int index, string[] suggestedCommands)
    {
        var commandsAsString = string.Join(" ", suggestedCommands);
        var prompt = _prompt + text;
        return string.Join('\n', text[index - 1], commandsAsString, prompt);
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