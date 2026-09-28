using Calibrator.Devices;
using Calibrator.GUI;
using HidSharp;
using Calibrator.Window;
using Calibrator.App;


DeviceManager deviceManager = new DeviceManager();
GUI gui = new GUI();


void Main()
{
    List<HidDevice> deviceSet = deviceManager.GetDevices();
    WriteDevices(deviceSet);

    App.Run(deviceManager);
}

void WriteDevices(List<HidDevice> devices)
{
    int deviceNum = 0;
    foreach (HidDevice device in devices)
    {
        Console.WriteLine($"    {deviceNum}: {device.GetProductName()} - {device.DevicePath}");
        deviceNum++;
    }
}

Main();
