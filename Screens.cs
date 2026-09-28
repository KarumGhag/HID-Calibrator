using HidSharp;
using Raylib_cs;
using Calibrator.Devices;
using System.Numerics;

namespace Calibrator.Screens;

public interface Screen
{
    void Update();
    void Draw();
    Screen? nextScreen { get; }
}

public class SelectDeviceScreen : Screen
{
    int selectedInt = 0;
    HidDevice selectedDevice;

    const int fontSize = 34;
    const int padding = 10;
    const int spacing = fontSize + padding;

    DeviceManager manager;
    List<String> deviceNames = new List<String>();

    public Screen? nextScreen { get; private set; } // others can get it, only this can set it

    Vector2 pos;
    readonly int centerX;
    readonly int firstY;

    public SelectDeviceScreen(DeviceManager manager, int monitorWidth, int monitorHeight)
    {
        this.manager = manager;
        if (manager!.deviceSet.Count == 0) throw new("No HID devices connected!");

        selectedDevice = manager.deviceSet[0];
        foreach (HidDevice device in manager.deviceSet)
        {
            deviceNames.Add(device.GetProductName());
        }

        centerX = monitorWidth / 2;
        firstY = monitorHeight / 2 - (manager.deviceSet.Count * spacing);

    }

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Right) || Raylib.IsKeyPressed(KeyboardKey.Down)) { selectedInt++; selectedInt %= deviceNames.Count; }
        if (Raylib.IsKeyPressed(KeyboardKey.Left) || Raylib.IsKeyPressed(KeyboardKey.Up)) { selectedInt--; if (selectedInt < 0) selectedInt = deviceNames.Count - 1; }
        selectedDevice = manager.deviceSet[selectedInt];
    }

    public void Draw()
    {
        int currentY = firstY;
        for (int i = 0; i < deviceNames.Count; i++)
        {
            int x = centerX - (Raylib.MeasureText(deviceNames[i], fontSize) / 2);
            currentY += spacing;
            Color colour = i == selectedInt ? Color.Green : Color.White;
            Raylib.DrawText(deviceNames[i], x, currentY, fontSize, colour);
        }
    }
}
