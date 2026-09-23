namespace CodeCrafters.Shell;

public class TypeCommand : ICommand
{
    public void Execute(string[] args)
    {
        var builtInCommands = Commands.GetBuiltInCommands();
        var argumentCmd = args.Length > 0 ? args[0] : string.Empty;

        if (builtInCommands.Contains(argumentCmd))
        {
            Console.WriteLine($"{argumentCmd} is a shell builtin");
        }
        else
        {
            var executablePaths = Commands.GetExecutablePaths();
            var found = false;

            foreach (var path in executablePaths)
            {
                if (!Directory.Exists(path))
                {
                    continue;
                }

                foreach (var filePath in Directory.EnumerateFiles(path))
                {
                    var fileInfo = new FileInfo(filePath);
                    var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                    if (fileNameWithoutExt.Equals(argumentCmd)
                        && fileInfo.UnixFileMode.HasFlag(
                            UnixFileMode.OtherExecute |
                            UnixFileMode.UserExecute |
                            UnixFileMode.GroupExecute))
                    {
                        Console.WriteLine($"{argumentCmd} is {fileInfo.FullName}");
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"{argumentCmd}: not found");
            }
        }
    }
}