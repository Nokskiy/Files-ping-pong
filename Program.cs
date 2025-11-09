namespace FilesGame;

public abstract class Program
{
    private static void Main()
    {
        var gameManager = new GameManager();
        Console.ReadLine();
        gameManager.Dispose();
    }
}