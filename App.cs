using Calibrator.Window;
using Calibrator.Devices;
using Calibrator.Screens;
using Raylib_cs;

namespace Calibrator.App;

public static class App
{
    public static void Run(DeviceManager manager)
    {
        using AppWindow window = new AppWindow();
        Screen currentScreen = new SelectDeviceScreen(manager, window.monitorWidth, window.monitorHeight);

        while (!window.ShouldClose())
        {
            currentScreen.Update();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            currentScreen.Draw();
            Raylib.EndDrawing();
        }
    }
}
