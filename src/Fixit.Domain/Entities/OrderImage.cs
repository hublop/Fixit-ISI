namespace Fixit.Domain.Entities
{
    public class OrderImage
    {
        public int ImageId { get; set; }
        public int OrderId { get; set; }

        public Image Image { get; set; }
        public Order Order { get; set; }
    }
}