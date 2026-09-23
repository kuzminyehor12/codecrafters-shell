namespace CodeCrafters.Shell;

public class CdCommand : ICommand
{
    public void Execute(string[] args)
    {
        var directoryName = args.Length > 0 ? args[0] : string.Empty;

        if (!Directory.Exists(directoryName))
        {
            Console.WriteLine($"cd: {directoryName}: No such file or directory");
            return;
        }

        if (directoryName.Equals("~"))
        {
            var homeDirectory = Environment.GetEnvironmentVariable("HOME");

            if (!string.IsNullOrWhiteSpace(homeDirectory) && Directory.Exists(homeDirectory))
            {
                Directory.SetCurrentDirectory(directoryName);
                return;
            }
        }

        Directory.SetCurrentDirectory(directoryName);
    }
}