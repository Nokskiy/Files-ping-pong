using System.Reflection;

namespace FilesGame;

public class GameManager : IDisposable
{
    private string? _gamePath;
    private const int FieldWidth = 6;
    private const int FieldHeight = 5;
    
    public GameManager()
    {
        CreateGameSession();
    }

    private void CreateGameSession()
    {
        Console.WriteLine("Session was created.");
        
        _gamePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "FilesGameSession");
        
        if(Directory.Exists(_gamePath))
            Directory.Delete(_gamePath, true);
        
        Directory.CreateDirectory(_gamePath);

        for (var i = 0; i < FieldHeight; i++)
        {
            var fileName = "-";
            for (var j = 0; j < FieldWidth; j++)
                fileName += ".";
            for (var j = 0; j <= i; j++)
                fileName += "-";
            fileName += "-";
            File.Create(Path.Combine(_gamePath,fileName)).Dispose();
        }
    }
    
    ~GameManager()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        Console.WriteLine("Session was closed.");
        if(_gamePath != null && Directory.Exists(_gamePath))
            Directory.Delete(_gamePath, true);
        
        GC.SuppressFinalize(this);
    }
}