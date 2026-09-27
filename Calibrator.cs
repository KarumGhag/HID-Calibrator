using HidSharp;

namespace Calibrator.Devices;

public class DeviceManager
{
    List<HidDevice>? deviceSet;

    public List<HidDevice> GetDevices()
    {
        deviceSet = new List<HidDevice>();
        IEnumerable<HidDevice> allDevices = DeviceList.Local.GetHidDevices();

        List<String> deviceNames = new List<String>();
        List<String> paths = new List<String>();

        Console.WriteLine("Devices:");
        foreach (HidDevice device in allDevices)
        {
            string name;
            try { name = device.GetProductName(); }
            catch { name = "(unknown)"; }

            if (deviceNames.Contains(name.ToLower())) continue;
            deviceSet.Add(device);
            deviceNames.Add(name.ToLower());
            paths.Add(device.DevicePath);
        }

        return deviceSet;
    }
}
