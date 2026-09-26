namespace CodeCrafters.Shell;

public static class FileUtilities
{
    public static bool IsExecutable(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
        {
            return false;
        }

        var fileInfo = new FileInfo(fileName);
        return fileInfo
            .UnixFileMode
            .HasFlag(
                UnixFileMode.OtherExecute 
                | UnixFileMode.GroupExecute 
                | UnixFileMode.UserExecute);
    }
}