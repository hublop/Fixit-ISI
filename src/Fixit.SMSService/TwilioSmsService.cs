using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Fixit.SmsService
{
    public class TwilioSmsService : ISmsService
    {
        private readonly string _senderNumber;
        private readonly bool _isSmsEnabled;

        public TwilioSmsService(IOptions<TwilioSmsServiceOptions> optionsAccessor)
        {
            var options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));

            _senderNumber = options.SenderPhoneNumber;
            _isSmsEnabled = options.SmsEnabled;

            TwilioClient.Init(options.AccountSid, options.AuthToken);
        }


        public async Task SendSmsAsync(string to, string content)
        {
            if (!_isSmsEnabled)
            {
                return;
            }

            await MessageResource.CreateAsync(to: to, from: _senderNumber, body: content);
        }
    }
}