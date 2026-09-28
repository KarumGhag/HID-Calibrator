using HidSharp;
using Raylib_cs;
using System.Numerics;

namespace Calibrator.GUI;

public class GUI
{

    List<HidDevice> devices = new List<HidDevice>();
    readonly List<String> deviceNames = new List<String>();


    public void UpdateDevices(List<HidDevice> newDevices)
    {
        devices = newDevices;
        foreach (HidDevice device in devices)
        {
            deviceNames.Add(device.GetProductName());
        }
    }

    public HidDevice GetDeviceFromName(string name)
    {
        foreach (HidDevice device in devices)
        {
            if (device.GetProductName() == name) return device;
        }

        return devices[0];
    }

    public void StartUI()
    {
        Raylib.InitWindow(1920, 1080, "Hello, World");

        int monitor = Raylib.GetCurrentMonitor();
        int monitorWidth = Raylib.GetMonitorWidth(monitor);
        int monitorHeight = Raylib.GetMonitorHeight(monitor);

        Raylib.SetWindowState(ConfigFlags.UndecoratedWindow);
        Raylib.SetWindowSize(monitorWidth, monitorHeight);
        Raylib.SetWindowPosition(0, 0);

        int deviceCount = deviceNames.Count;
        int fontSize = 34;
        int spacing = fontSize + 10; // a bit of padding between lines looks better than exact font size
        int totalHeight = deviceCount * spacing;

        Vector2 start = new Vector2(monitorWidth / 2, (monitorHeight / 2) - (totalHeight / 2));

        int selectedInt = 0;
        HidDevice? selected = devices[selectedInt];

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            int yPos = (int)start.Y;
            foreach (String name in deviceNames)
            {
                int xPos = (int)(start.X - (Raylib.MeasureText(name, fontSize) / 2)); // middle on x
                Raylib.DrawText(name, xPos, yPos, fontSize, (GetDeviceFromName(name) == selected) ? Color.Green : Color.White);
                yPos += spacing;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Right) || Raylib.IsKeyPressed(KeyboardKey.Down)) { selectedInt++; selectedInt %= deviceCount; }
            if (Raylib.IsKeyPressed(KeyboardKey.Left) || Raylib.IsKeyPressed(KeyboardKey.Up)) { selectedInt--; if (selectedInt < 0) selectedInt = deviceCount - 1; }
            selected = devices[selectedInt];

            Raylib.EndDrawing();
        }
    }
}
