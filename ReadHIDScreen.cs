using System.Numerics;
using HidSharp;
using System.Text;
using Raylib_cs;
using Calibrator.Screens;
using Calibrator.Devices;

namespace Calibrator.ReadHidScreen;

public class ReadInputScreen : Screen
{
    public Screen? nextScreen { get; }
    HidDevice? selectedDevice;

    string? incoming;

    public ReadInputScreen(HidDevice device)
    {
        selectedDevice = device;
    }

    public void Update()
    {
        HidStream stream = selectedDevice!.Open();
        byte[] buffer = new byte[selectedDevice.GetMaxInputReportLength()];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        incoming = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
    }

    public void Draw()
    {
        int textLen = Raylib.MeasureText(incoming, 35);
        Raylib.DrawText(incoming, 1920 / 2 - textLen / 2, 1080 / 2, 35, Color.White);
    }

    public void NextScreen() {}
}
