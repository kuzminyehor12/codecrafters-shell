namespace CodeCrafters.Shell;

public interface ICommand
{
    public void Execute(string[] args);
}