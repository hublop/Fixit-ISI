namespace Fixit.WebApi.Orders.DTOs
{
    public class AcceptOrderCommandDto
    {
        public int ContractorId { get; set; }
        public string Comment { get; set; }
        public double PredictedPrice { get; set; }
    }
}
