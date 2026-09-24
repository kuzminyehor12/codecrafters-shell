namespace CodeCrafters.Shell;

public class Output
{
    public TextWriter StandardOutput { get; private set; } = null!;

    public int OutRedirectionIndex { get; private set; } = -1;

    public TextWriter StandardError { get; private set; } = null!;

    public int ErrRedirectionIndex { get; private set; } = -1;

    public static Output Create(string[] args)
    {
        var output = new Output();

        var resolvedShorthandOutput = ResolveArgs(args);
        var standardOutputInfo = resolvedShorthandOutput.RedirectionIndex == -1 
            ? ResolveArgs(args, "1>") 
            : resolvedShorthandOutput;

        output.StandardOutput = standardOutputInfo.Writer;
        output.OutRedirectionIndex = standardOutputInfo.RedirectionIndex;

        var errorOutputInfo = ResolveArgs(args, "2>");

        output.StandardError = errorOutputInfo.Writer;
        output.ErrRedirectionIndex = errorOutputInfo.RedirectionIndex;

        return output;
    }

    private static (TextWriter Writer, int RedirectionIndex) ResolveArgs(string[] args, string redirectionOperator = ">")
    {
        int redirectionIndex = args.ToList().IndexOf(redirectionOperator);

        if (redirectionIndex == -1)
        {
            return (Console.Out, redirectionIndex);
        }

        var fileName = args.Length > redirectionIndex + 1 ? args[redirectionIndex + 1] : string.Empty;

        if (string.IsNullOrEmpty(fileName))
        {
            return (Console.Out, redirectionIndex);
        }

        var fileStream = new FileStream(fileName, FileMode.Create);
        return (new StreamWriter(fileStream), redirectionIndex);
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