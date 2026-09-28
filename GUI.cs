using HidSharp;
using Raylib_cs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Sockets;
using System.Numerics;

namespace Calibrator.GUI;

public class GUI
{

    List<HidDevice> devices = new List<HidDevice>();
    List<String> deviceNames = new List<String>();


    public void UpdateDevices(List<HidDevice> newDevices)
    {
        devices = newDevices;
        deviceNames = new List<String>();

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

        SelectionMenu selectionMenu = new SelectionMenu(this, monitorWidth, monitorHeight);
        selectionMenu.UpdateDevices(devices, deviceNames);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            selectionMenu.Update();

            Raylib.EndDrawing();
        }
    }
}

public class Menu
{
    protected readonly GUI gui;

    protected List<HidDevice> devices;
    protected List<String> deviceNames;

    protected readonly int monitorWidth;
    protected readonly int monitorHeight;

    public Menu(GUI gui, int monitorWidth, int monitorHeight)
    {
        this.gui = gui;
        this.monitorWidth = monitorWidth;
        this.monitorHeight = monitorHeight;
    }

    public virtual void UpdateDevices(List<HidDevice> devices, List<String> deviceNames)
    {
        this.devices = devices;
        this.deviceNames = deviceNames;
    }

    public virtual void Update() {}
}

public class SelectionMenu : Menu
{
    int selectedInt = 0;

    int deviceCount;
    readonly int fontSize = 34;
    int spacing = 10; // a bit of padding between lines looks better than exact font size
    int totalHeight;

    Vector2 topTextPos = new Vector2();


    public SelectionMenu(GUI gui, int monitorWidth, int monitorHeight) : base(gui, monitorWidth, monitorHeight) {}

    public override void UpdateDevices(List<HidDevice> devices, List<String> deviceNames)
    {
        base.UpdateDevices(devices, deviceNames);

        deviceCount = devices.Count;
        spacing += fontSize;
        totalHeight = deviceCount * spacing;
        topTextPos = new Vector2(monitorWidth / 2, (monitorHeight / 2) - (totalHeight / 2));
    }

    public override void Update()
    {
        HidDevice? selected = devices[selectedInt];

        int yPos = (int)topTextPos.Y;
        foreach (String name in deviceNames)
        {
            int xPos = (int)(topTextPos.X - (Raylib.MeasureText(name, fontSize) / 2)); // middle on x
            Raylib.DrawText(name, xPos, yPos, fontSize, (gui.GetDeviceFromName(name) == selected) ? Color.Green : Color.White);
            yPos += spacing;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Right) || Raylib.IsKeyPressed(KeyboardKey.Down)) { selectedInt++; selectedInt %= deviceCount; }
        if (Raylib.IsKeyPressed(KeyboardKey.Left) || Raylib.IsKeyPressed(KeyboardKey.Up)) { selectedInt--; if (selectedInt < 0) selectedInt = deviceCount - 1; }
        selected = devices[selectedInt];
    }
}
