using System.Reflection;
using System.Timers;
using FilesGame.Objects;
using Timer = System.Timers.Timer;
using Object = FilesGame.Objects.Object;
using System.Runtime.InteropServices;

namespace FilesGame;

public class GameManager : IDisposable
{
    private string _gamePath;
    private const int FieldWidth = 10;
    private const int FieldHeight = 8;
    private Timer _timer;

    private List<Object> _objects = new()
    {
        new Ball
        {
            Position = new(4, 4)
        }
    };

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
        _timer = new Timer(1000);
        _timer.Elapsed += Update;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void CreateGameSession()
    {
        if (FieldHeight > _supportedExtensions.Length) return;

        Console.WriteLine("Session was created.");

        _gamePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "FilesGameSession");

        if (Directory.Exists(_gamePath))
            Directory.Delete(_gamePath, true);

        Directory.CreateDirectory(_gamePath);

        for (var i = 0; i < FieldHeight; i++)
        {
            var fileName = "";
            for (var j = 0; j < FieldWidth; j++)
                fileName += "–";

            fileName += "." + _supportedExtensions[i];

            var path = Path.Combine(_gamePath, fileName);
            File.Create(path).Dispose();
        }
    }

    private void Update(object? sender, ElapsedEventArgs elapsedEventArgs)
    {
        FillField();
    }

    private void FillField()
    {
        var newFiles = new string[FieldHeight];
        for (var y = 0; y < FieldHeight; y++)
        {
            var newFileName = "";
            
            for (var x = 0; x < FieldWidth; x++)
            {
                string? newChar = null;

                foreach (var obj in _objects.Where(obj => (int)obj.Position.X == x && (int)obj.Position.Y == y))
                    newChar = obj.RenderSymbol;
                newChar ??= "–";
                newFileName += newChar;
            }

            var newPath = Path.Combine(_gamePath, newFileName + "." + _supportedExtensions[y]);
            newFiles[y] = newPath;
        }

        foreach (var i in Directory.GetFiles(_gamePath))
            File.Delete(i);
        foreach (var i in newFiles)
            File.Create(i).Dispose();
        
        ShellApi.RefreshAllExplorerWindows();
    }

    ~GameManager()
    {
        Dispose();
    }

    public void Dispose()
    {
        Console.WriteLine("Session was closed.");
        if (_gamePath != null && Directory.Exists(_gamePath))
            Directory.Delete(_gamePath, true);

        GC.SuppressFinalize(this);
    }
}