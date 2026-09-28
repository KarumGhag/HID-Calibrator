using Calibrator.Devices;
using Calibrator.GUI;
using HidSharp;

DeviceManager deviceManager = new DeviceManager();
GUI gui = new GUI();

void Main()
{
    List<HidDevice> deviceSet = deviceManager.GetDevices();
    WriteDevices(deviceSet);
    gui.UpdateDevices(deviceSet);
    gui.StartUI();
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
