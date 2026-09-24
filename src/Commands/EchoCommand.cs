namespace CodeCrafters.Shell;

public class EchoCommand : ICommand
{
    public void Execute(string[] args)
    {
        var outStream = Output.Resolve(args, out int redirectionIndex);
        var output = string.Join(' ', redirectionIndex != -1 ? args[..redirectionIndex] : args);
        outStream.WriteLine(output);
        Output.Flush(outStream);
    }
}