namespace Fixit.Infrastructure.Services.Sms
{
    public class TwilioSmsServiceOptions
    {
        public string SenderPhoneNumber { get; set; }
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public bool SmsEnabled { get; set; }
    }
}