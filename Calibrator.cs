using HidSharp;

namespace Calibrator.Devices;

public class DeviceManager
{
    List<HidDevice>? deviceSet = new List<HidDevice>();
    public List<String>? deviceNames  = new List<String>();

    public List<HidDevice> GetDevices()
    {
        IEnumerable<HidDevice> allDevices = DeviceList.Local.GetHidDevices();

        List<String> paths = new List<String>();

        Console.WriteLine("Devices:");
        foreach (HidDevice device in allDevices)
        {
            string name;
            try { name = device.GetProductName(); }
            catch { name = "(unknown)"; }

            if (name == "") continue;
            if (deviceNames!.Contains(name.ToLower())) continue;
            deviceSet!.Add(device);
            deviceNames.Add(name.ToLower());
            paths.Add(device.DevicePath);
        }

        return deviceSet!;
    }
}
