using System.Text.RegularExpressions;
using HidLibrary;

namespace StudioDisplayBright.HID
{
    public static class HIDHandler
    {
        private const int VendorId = 0x05ac;
        private const int ProductId = 0x1114;

        public static IEnumerable<HidDevice?> GetStudioDisplaysWithInterfaceNumber7()
        {
            var devices = HidDevices.Enumerate(VendorId, ProductId)
                .Where(device => ExtractInterfaceNumber(device.DevicePath) == 7);

            foreach (var device in devices)
            {
                Console.WriteLine($"Device Path: {device.DevicePath}");
               Console.WriteLine($"Interface Number: 7");
            }

            return devices;
        }

        private static int? ExtractInterfaceNumber(string devicePath)
        {
            var match = Regex.Match(devicePath, @"mi_(\d+)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int interfaceNumber))
            {
                return interfaceNumber;
            }
            return null; // Keine Interface-Nummer gefunden
        }
    }
}