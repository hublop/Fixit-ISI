namespace Fixit.Infrastructure.Services.OrdersNotifierWorker
{
    public class OrdersNotifierWorkerServiceOptions
    {
        public int ScanningIntervalInMiliseconds { get; set; }
        public int TimeAfterNonPremiumGetNotificationInMinutes { get; set; }
    }
}