using System.Numerics;
using HidSharp;
using System.Text;
using Raylib_cs;
using Calibrator.Screens;
using Calibrator.Devices;
using System.IO.Pipes;

namespace Calibrator.ReadHidScreen;

public class ReadInputScreen : Screen
{
    public Screen? nextScreen { get; }
    HidDevice? selectedDevice;

    string? incoming;
    string[] incomingList;
    int incomingLen;

    readonly HidStream stream;

    readonly int textLen;

    readonly int fontSize = 35;
    readonly int centerX = 1920 / 2;
    readonly int centerY = 1080 / 2;

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

        textLen = Raylib.MeasureText("  FF  ", fontSize);
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
        DrawVertical();
    }

    public void DrawHorizontal()
    {
        int verticalPadding = 45;
        int leftStart = (centerX) - (incomingLen / 2 * textLen);

        for (int i = 0; i < incomingLen; i++)
        {
            string byteLabel = $"|{(i + 1).ToString()}|";
            int xPos = leftStart + textLen * i;
            Raylib.DrawText(incomingList[i], xPos, centerY, fontSize, Color.White);
            Raylib.DrawText(byteLabel, xPos, centerY - verticalPadding, fontSize, Color.White);
        }
    }

    public void DrawVertical()
    {
        int horizontalPadding = 20;
        int yStart = 35 * incomingLen / 2;

        for (int i = 0; i < incomingLen; i++)
        {
            string byteLabel = $"{(i + 1).ToString()}:";
            int byteLabelLen = Raylib.MeasureText(byteLabel, fontSize);
            int yPos = yStart + 35 * i;
            Raylib.DrawText(incomingList[i], centerX - textLen / 2, yPos, fontSize, Color.White);
            Raylib.DrawText(byteLabel, centerX - byteLabelLen - textLen - horizontalPadding, yPos, fontSize, Color.White);

        }

    }

    public void NextScreen() {}
}
