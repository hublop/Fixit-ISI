namespace Fixit.EventBus.RabbitMQ
{
    public class EventBusOptions
    {
        public string Uri { get; set; }
        public int RetryCount { get; set; }
        public string SubscriptionClientName { get; set; }
    }
}