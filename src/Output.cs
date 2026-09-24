namespace CodeCrafters.Shell;

public class Output
{
    private static readonly string[] RedirectionOperators = [">", "1>", "2>", ">>", "1>>", "2>>"];

    public TextWriter StandardOutput { get; private set; } = null!;

    public int OutRedirectionIndex { get; private set; } = -1;

    public TextWriter StandardError { get; private set; } = null!;

    public int ErrRedirectionIndex { get; private set; } = -1;

    public static Output Create(string[] args)
    {
        var output = new Output();

        if (HasOutRedirection(args, out string? redirectionOperator))
        {
            var standardOutputInfo = ResolveArgs(args, redirectionOperator!);
            output.StandardOutput = standardOutputInfo.Writer;
            output.OutRedirectionIndex = standardOutputInfo.RedirectionIndex;
        }
        else
        {
            output.StandardOutput = Console.Out;
        }

        if (HasErrRedirection(args, out string? errRedirectionOperator))
        {
            var errorOutputInfo = ResolveArgs(args, errRedirectionOperator!);
            output.StandardError = errorOutputInfo.Writer;
            output.ErrRedirectionIndex = errorOutputInfo.RedirectionIndex;
        }
        else
        {
            output.StandardError = Console.Out;
        }  

        return output;
    }

    private static (TextWriter Writer, int RedirectionIndex) ResolveArgs(string[] args, string redirectionOperator)
    {
        int redirectionIndex = args.IndexOf(redirectionOperator);

        if (redirectionIndex == -1)
        {
            return (Console.Out, redirectionIndex);
        }

        var fileName = args.Length > redirectionIndex + 1 ? args[redirectionIndex + 1] : string.Empty;

        if (string.IsNullOrEmpty(fileName))
        {
            return (Console.Out, redirectionIndex);
        }

        var fileMode = RedirectionOperators.Contains(redirectionOperator) 
            && new string[] { ">", "1>", "2>" }.Contains(redirectionOperator) 
                ? FileMode.Create 
                : FileMode.Append;

        var fileStream = new FileStream(fileName, fileMode);
        return (new StreamWriter(fileStream), redirectionIndex);
    }

    private static bool HasOutRedirection(string[] args, out string? redirectionOperator)
    {
        string[] outRedirectionOperators = [">", "1>", ">>", "1>>"];
        redirectionOperator = null;

        foreach (var op in outRedirectionOperators)
        {
            if (args.Contains(op))
            {
                redirectionOperator = op;
                return true;
            }
        }

        return false;
    }

    private static bool HasErrRedirection(string[] args, out string? redirectionOperator)
    {
        string[] errRedirectionOperators = ["2>", "2>>"];
        redirectionOperator = null;

        foreach (var op in errRedirectionOperators)
        {
            if (args.Contains(op))
            {
                redirectionOperator = op;
                return true;
            }
        }

        return false;
    }


    public void Flush()
    {
        StandardOutput.Flush();
        StandardError.Flush();

        if (!ReferenceEquals(StandardOutput, Console.Out))
        {
            StandardOutput.Dispose();
        }

        if (!ReferenceEquals(StandardError, Console.Out))
        {
            StandardError.Dispose();
        }
    }
}