using Raylib_cs;

namespace Calibrator.Window;

// this class only holds the window, knows nothing about data
// sealed to remove warning about Dispose() just means that this cannot be inhertied
// IDisposable means that once I stop using it dispose gets called
public sealed class AppWindow : IDisposable
{
    readonly int monitor;
    public readonly int monitorWidth;
    public readonly int monitorHeight;


    public AppWindow()
    {
        Raylib.InitWindow(1920, 1080, "HID Calibrator");
        Raylib.SetWindowState(ConfigFlags.UndecoratedWindow);
        Raylib.SetWindowSize(monitorWidth, monitorHeight);

        monitor = Raylib.GetCurrentMonitor();
        monitorWidth = Raylib.GetMonitorWidth(monitor);
        monitorHeight = Raylib.GetMonitorHeight(monitor);
        Raylib.SetWindowPosition(0, 0);

        Raylib.SetWindowState(ConfigFlags.UndecoratedWindow);
        Raylib.SetWindowSize(monitorWidth, monitorHeight);
        Raylib.SetWindowPosition(0, 0);
    }

    public bool ShouldClose() { return Raylib.WindowShouldClose(); }
    public void Dispose()
    {
        Raylib.CloseWindow();
    }
}
