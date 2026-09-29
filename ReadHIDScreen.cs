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
    string[] incomingList;
    int incomingLen;

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

        textLen = Raylib.MeasureText("  FF  ", 35);
    }

//   FF       FF       FF       FF       FF       FF       FF       FF       FF       FF       FF

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

        incomingList = incoming.Split("-");
        incomingLen = incomingList.Length;
    }

    public void Draw()
    {
        int leftStart = (1920 / 2)  - (incomingLen / 2 * textLen);

        for (int i = 0; i < incomingLen; i++)
        {
            string byteLabel = $"|{(i + 1).ToString()}|";
            int byteLabelLen = Raylib.MeasureText(byteLabel, 35) / 2;
            int xPos = leftStart + textLen * i;
            Raylib.DrawText(incomingList[i], xPos, 1080 / 2, 35, Color.White);
            Raylib.DrawText(byteLabel, xPos, 1080 / 2 - 45, 35, Color.White);
        }
    }

    public void NextScreen() {}
}
