namespace CodeCrafters.Shell;

public class EchoCommand : ICommand
{
    public void Execute(string[] args)
    {
        var output = Output.Create(args);
        var outputData = string.Join(' ', output.OutRedirectionIndex != -1 
            ? args[..output.OutRedirectionIndex] 
            : output.ErrRedirectionIndex != -1 
                ? args[..output.ErrRedirectionIndex]
                : args);
        output.StandardOutput.WriteLine(outputData);
        output.Flush();
    }
}