namespace Fixit.Infrastructure
{
    public static class Constants
    {
        public static class ConfigSections
        {
            public const string SendGrid = "SendGrid";
            public const string Twilio = "Twilio";
            public const string Cloudinary = "Cloudinary";
            public const string OrdersNotifier = "OrdersNotifierWorker";
        }
    }
}