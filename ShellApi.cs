using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FilesGame;

public class ShellApi
{
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
        string lpszClass, string lpszWindow);

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("shell32.dll")]
    private static extern int SHChangeNotify(int eventId, uint flags, IntPtr item1, IntPtr item2);

    private const uint WM_COMMAND = 0x0111;
    private const int SHCNE_ASSOCCHANGED = 0x08000000;
    private const uint SHCNF_FLUSH = 0x1000;

    public static void RefreshAllExplorerWindows()
    {
        SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, 0, 0);

        RefreshAllExplorerInstances();
    }

    private static void RefreshAllExplorerInstances()
    {
        foreach (var explorer in Process.GetProcessesByName("explorer"))
            if (explorer.MainWindowHandle != 0)
                SendMessage(explorer.MainWindowHandle, WM_COMMAND, 0x7102, 0);
    }
}