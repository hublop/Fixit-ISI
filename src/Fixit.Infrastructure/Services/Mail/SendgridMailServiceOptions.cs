namespace Fixit.Infrastructure.Services.Mail
{
    public class SendgridMailServiceOptions
    {
        public string ApiKey { get; set; }
        public string DefaultSenderEmail { get; set; }
    }
}