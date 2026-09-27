using Calibrator.Devices;
using HidSharp;

DeviceManager deviceManager = new DeviceManager();
List<HidDevice> deviceSet = deviceManager.GetDevices();

void Main()
{
    WriteDevices(deviceSet);
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
