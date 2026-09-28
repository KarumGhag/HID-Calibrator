using Raylib_cs;

namespace Calibrator.Window;

public class Window
{
    readonly int monitor;
    readonly int monitorWidth;
    readonly int monitorHeight;

    public Window()
    {
        monitor = Raylib.GetCurrentMonitor();
        monitorWidth = Raylib.GetMonitorWidth(monitor);
        monitorHeight = Raylib.GetMonitorHeight(monitor);
    }

    public void StartWindow()
    {
        Raylib.InitWindow(1920, 1080, "HID Calibrator");
        Raylib.SetWindowState(ConfigFlags.UndecoratedWindow);
        Raylib.SetWindowSize(monitorWidth, monitorHeight);
    }
}
