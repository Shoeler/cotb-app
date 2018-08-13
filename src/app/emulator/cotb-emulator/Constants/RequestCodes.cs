namespace Cotb.Emulator.Constants
{
    public static class RequestCodes
    {
        public const string GRILL_INFO = "UR001!";
        public const string SET_GRILL_TEMP = @"UT(\d{3})!";
        public const string SET_PROBE_TEMP = @"UF(\d{3})!";
        public const string POWER_ON = "UK001!";
        public const string POWER_OFF = "UK004!";
        public const string GRILL_ID = "UL!";
        public const string GRILL_FIRMWARE = "UN!";
        public const string TODO = "UA!";
    }
}