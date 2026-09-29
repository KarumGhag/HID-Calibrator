using Calibrator.Devices;
using HidSharp;
using Calibrator.App;


DeviceManager deviceManager = new DeviceManager();

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

void Test()
{

}

Main();
