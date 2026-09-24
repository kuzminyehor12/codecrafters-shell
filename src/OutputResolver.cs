namespace CodeCrafters.Shell;

public class OutputResolver
{
    public static TextWriter Resolve(string[] args, out int redirectionIndex)
    {
        redirectionIndex = args.IndexOf(">") != -1 ? args.IndexOf(">") : args.IndexOf("1>");

        if (redirectionIndex == -1)
        {
            return Console.Out;
        }

        var fileName = args.Length > redirectionIndex + 1 ? args[redirectionIndex + 1] : string.Empty;

        if (string.IsNullOrEmpty(fileName))
        {
            return Console.Out;
        }

        var fileStream = new FileStream(fileName, FileMode.Create);
        return new StreamWriter(fileStream);
    }

    public static void Commit(TextWriter writer)
    {
        writer.Flush();

        if (!ReferenceEquals(writer, Console.Out))
        {
            writer.Dispose();
        }
    }
}