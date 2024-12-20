using HidLibrary;

namespace StudioDisplayBright.HID
{
    public static class BrightnessController
    {
        private const byte ReportId = 1;
        private const uint MinBrightness = 400;
        private const uint MaxBrightness = 60000;
        private static readonly uint BrightnessRange = MaxBrightness - MinBrightness;

        public static uint GetBrightness(HidDevice device)
        {
            var buffer = new byte[7];
            buffer[0] = ReportId;

            device.ReadFeatureData(out buffer);
            return BitConverter.ToUInt32(buffer, 1);
        }

        public static byte GetBrightnessPercent(HidDevice device)
        {
            var brightness = GetBrightness(device) - MinBrightness;
            return (byte)((brightness / (float)BrightnessRange) * 100);
        }

        public static void SetBrightness(HidDevice device, uint brightness)
        {
            var buffer = new byte[7];
            buffer[0] = ReportId;
            Array.Copy(BitConverter.GetBytes(brightness), 0, buffer, 1, 4);

            device.WriteFeatureData(buffer);
        }

        public static void SetBrightnessPercent(HidDevice device, byte percent)
        {
            var nits = (uint)(MinBrightness + (percent / 100.0f * BrightnessRange));
            nits = Math.Min(MaxBrightness, Math.Max(MinBrightness, nits));
            SetBrightness(device, nits);
        }
    }
}