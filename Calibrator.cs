using HidSharp;

namespace Calibrator.Devices;

public class DeviceManager
{
    public readonly List<HidDevice> deviceSet = new List<HidDevice>();
    readonly List<String> deviceNames = new List<String>();
    readonly List<String> paths = new List<String>();

    public HidDevice selectedDevice;

    public List<HidDevice> GetDevices()
    {
        IEnumerable<HidDevice> allDevices = DeviceList.Local.GetHidDevices();


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
