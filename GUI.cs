using HidSharp;
using Raylib_cs;
using System.Numerics;

namespace Calibrator.GUI;

public class GUI
{

    List<HidDevice> devices = new List<HidDevice>();


    public void SetDevices(List<HidDevice> newDevices)
    {
        devices = newDevices;
    }

    public HidDevice GetDeviceFromName(String name)
    {
        foreach (HidDevice device in devices)
        {
            if (device.GetProductName() == name) return device;
        }

        return devices[0];
    }

    public void StartUI()
    {
        Raylib.InitWindow(1920, 1080, "Calibrator");

        int monitor = Raylib.GetCurrentMonitor();
        int monitorWidth = Raylib.GetMonitorWidth(monitor);
        int monitorHeight = Raylib.GetMonitorHeight(monitor);

        Raylib.SetWindowState(ConfigFlags.UndecoratedWindow);
        Raylib.SetWindowSize(monitorWidth, monitorHeight);
        Raylib.SetWindowPosition(0, 0);

        SelectionMenu selectionMenu = new SelectionMenu(this, monitorWidth, monitorHeight);
        selectionMenu.SetDevices(devices);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            selectionMenu.SetDevices(devices);
            selectionMenu.Update();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}

public class Menu
{
    protected readonly GUI gui;


    protected readonly int monitorWidth;
    protected readonly int monitorHeight;

    protected List<HidDevice> devices;

    public Menu(GUI gui, int monitorWidth, int monitorHeight)
    {
        this.gui = gui;
        this.monitorWidth = monitorWidth;
        this.monitorHeight = monitorHeight;
    }

    public virtual void SetDevices(List<HidDevice> devices) { this.devices = devices;  }

    public virtual void Update() {}
}

public class SelectionMenu : Menu
{
    int selectedInt = 0;
    HidDevice? selected;

    int deviceCount;
    readonly int fontSize = 34;
    readonly int padding = 10;
    int spacing;
    int totalHeight;

    Vector2 topTextPos = new Vector2();

    public SelectionMenu(GUI gui, int monitorWidth, int monitorHeight) : base(gui, monitorWidth, monitorHeight) {}

    public override void SetDevices(List<HidDevice> devices)
    {
        base.SetDevices(devices);

        deviceCount = devices.Count;
        spacing = padding + fontSize;
        totalHeight = deviceCount * spacing;
        topTextPos = new Vector2(monitorWidth / 2, (monitorHeight / 2) - (totalHeight / 2));
    }

    public override void Update()
    {

        if (Raylib.IsKeyPressed(KeyboardKey.Right) || Raylib.IsKeyPressed(KeyboardKey.Down)) { selectedInt++; selectedInt %= deviceCount; }
        if (Raylib.IsKeyPressed(KeyboardKey.Left) || Raylib.IsKeyPressed(KeyboardKey.Up)) { selectedInt--; if (selectedInt < 0) selectedInt = deviceCount - 1; }
        selected = devices[selectedInt];

        int yPos = (int)topTextPos.Y;
        foreach (HidDevice device in devices)
        {
            String name = device.GetProductName();
            int xPos = (int)(topTextPos.X - (Raylib.MeasureText(name, fontSize) / 2)); // middle on x
            Raylib.DrawText(name, xPos, yPos, fontSize, (gui.GetDeviceFromName(name) == selected) ? Color.Green : Color.White);
            yPos += spacing;
        }

    }
}
