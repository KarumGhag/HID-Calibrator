using System.Numerics;
using Raylib_cs;
using Calibrator.Window;
using Calibrator.Devices;
using Calibrator.Screens;
using Calibrator.SelectionScreen;

namespace Calibrator.App;

public static class App
{
    static readonly String programName = "HID Calibration Device";
    static readonly int nameFontSize = 50;

    public static void Run(DeviceManager manager)
    {
        using AppWindow window = new AppWindow(programName);
        Screen currentScreen = new SelectDeviceScreen(manager, window.monitorWidth, window.monitorHeight);

        int nameLen = Raylib.MeasureText(programName, nameFontSize);
        Vector2 nameDrawPos = new Vector2(window.monitorWidth / 2 - nameLen / 2, 40);

        int underLineXPadding = 50;
        int underLineYPadding = 10;
        int underLineThickness = 5;
        Vector2 lineStartPos = new Vector2(nameDrawPos.X - underLineXPadding, nameDrawPos.Y + nameFontSize + underLineYPadding);
        Vector2 lineEndPos = new Vector2(window.monitorWidth / 2 + nameLen / 2 + underLineXPadding, nameDrawPos.Y + nameFontSize + underLineYPadding);



        while (!window.ShouldClose())
        {
            currentScreen.Update();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText(programName, (int)nameDrawPos.X, (int)nameDrawPos.Y, nameFontSize, Color.White);
            Raylib.DrawLineEx(lineStartPos, lineEndPos, underLineThickness, Color.White);
            currentScreen.Draw();
            Raylib.EndDrawing();
        }
    }
}
