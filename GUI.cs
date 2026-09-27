using Raylib_cs;
using System.Numerics;

namespace Calibrator.GUI;

public class GUI
{
    public void StartUI(List<String> deviceNames)
    {
        Raylib.InitWindow(1920, 1080, "Hello, World");

        int monitor = Raylib.GetCurrentMonitor();
        int monitorWidth = Raylib.GetMonitorWidth(monitor);
        int monitorHeight = Raylib.GetMonitorHeight(monitor);

        Raylib.SetWindowState(ConfigFlags.UndecoratedWindow);
        Raylib.SetWindowSize(monitorWidth, monitorHeight);
        Raylib.SetWindowPosition(0, 0);

        int deviceCount = deviceNames.Count();
        int fontSize = 34;
        int spacing = fontSize + 10; // a bit of padding between lines looks better than exact font size
        int totalHeight = deviceCount * spacing;

        Vector2 start = new Vector2(monitorWidth / 2, (monitorHeight / 2) - (totalHeight / 2));

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            int yPos = (int)start.Y; // reset every frame

            foreach (String name in deviceNames)
            {
                int xPos = (int)(start.X - (Raylib.MeasureText(name, fontSize) / 2));
                Raylib.DrawText(name, xPos, yPos, fontSize, Color.White);
                yPos += spacing; // going DOWN the list now, not up
            }

            Raylib.EndDrawing();
        }
    }
}
