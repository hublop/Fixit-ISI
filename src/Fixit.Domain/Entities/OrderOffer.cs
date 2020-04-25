namespace Fixit.Domain.Entities
{
    public class OrderOffer : Entity
    {
        public string Comment { get; set; }
        public double PredictedPrice { get; set; }

        public int OrderId { get; set; }
        public int ContractorId { get; set; }

        public Order Order { get; set; }
        public Contractor Contractor { get; set; }
    }
}