using Fixit.Shared.CQRS;

namespace Fixit.Application.Orders.Commands.NotifyAboutOrder
{
    public class NotifyAboutOrdersCommand : ICommand
    {
        public int TimeAfterNonPremiumGetNotificationInMinutes { get; set; }
    }
}