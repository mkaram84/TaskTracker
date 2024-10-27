namespace TaskTracker.App;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided.");
            return;
        }

        foreach (var arg in args)
        {
            Console.WriteLine($"Argument: {arg}");
        }

        Console.WriteLine("Hello, World!");
    }
}
