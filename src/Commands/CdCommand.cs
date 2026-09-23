namespace CodeCrafters.Shell;

public class CdCommand : ICommand
{
    public void Execute(string[] args)
    {
        var directoryName = args.Length > 0 ? args[0] : string.Empty;

        if (directoryName.Equals("~"))
        {
            var homeDirectory = Environment.GetEnvironmentVariable("HOME");

            if (!string.IsNullOrWhiteSpace(homeDirectory) && Directory.Exists(homeDirectory))
            {
                Directory.SetCurrentDirectory(homeDirectory);
                return;
            }
        }

        if (!Directory.Exists(directoryName))
        {
            Console.WriteLine($"cd: {directoryName}: No such file or directory");
            return;
        }

        Directory.SetCurrentDirectory(directoryName);
    }
}