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
    HidStream stream;

    public ReadInputScreen(HidDevice device)
    {
        selectedDevice = device;
        try
        {
            stream = selectedDevice!.Open();
        }
        catch
        {
            Console.WriteLine("stream failed");
        }


    }

    public void Update()
    {
        if (stream == null) return;

        byte[] buffer = new byte[selectedDevice!.GetMaxInputReportLength()];
        try
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0) return;
            incoming = BitConverter.ToString(buffer, 0, bytesRead);
        }
        catch (TimeoutException)
        {
            Console.WriteLine("timeout");
            return;
        }

    }

    public void Draw()
    {
        int textLen = Raylib.MeasureText(incoming, 35);
        Raylib.DrawText(incoming, 1920 / 2 - textLen / 2, 1080 / 2, 35, Color.White);
    }

    public void NextScreen() {}
}
