namespace CodeCrafters.Shell;

public static class BackgroundJobQueue
{
    public static Queue<int> JobQueue = new Queue<int>();

    public static void Enqueue(int pid)
    {
        JobQueue.Enqueue(pid);
    }
}