using System.Reflection;

namespace FilesGame;

public class GameManager : IDisposable
{
    private string? _gamePath;
    private const int FieldWidth = 10;
    private const int FieldHeight = 8;

    private readonly string[] _supportedExtensions =
    {
        "txt",
        "jpg",
        "jpeg",
        "png",
        "bmp",
        "mp3",
        "wav",
        "wma",
        "flac"
    };
    
    public GameManager()
    {
        CreateGameSession();
    }

    private void CreateGameSession()
    {
        if (FieldHeight > _supportedExtensions.Length) return;
        
        Console.WriteLine("Session was created.");
        
        _gamePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "FilesGameSession");
        
        if(Directory.Exists(_gamePath))
            Directory.Delete(_gamePath, true);
        
        Directory.CreateDirectory(_gamePath);

        for (var i = 0; i < FieldHeight; i++)
        {
            var fileName = "";
            for (var j = 0; j < FieldWidth; j++)
                fileName += "-";
            
            fileName += "." + _supportedExtensions[i];
            
            var path = Path.Combine(_gamePath, fileName);
            File.Create(path).Dispose();
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