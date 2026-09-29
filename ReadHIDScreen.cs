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

    int textLen;

    public ReadInputScreen(HidDevice device)
    {
        selectedDevice = device;
        try
        {
            stream = selectedDevice!.Open();
        }
        catch
        {
            throw new("stream failed");
        }

        int maxReportLength = selectedDevice.GetMaxInputReportLength();
        string longestText = "";
        for (int i = 0; i < maxReportLength; i++) longestText += "0";

        textLen = Raylib.MeasureText(longestText, 35);
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
        Raylib.DrawText(incoming, 1920 / 2 - textLen, 1080 / 2, 35, Color.White);
    }

    public void NextScreen() {}
}
