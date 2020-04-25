using System.Collections.Generic;

namespace Fixit.Domain.Entities
{
    public class RepairService
    {
        public RepairService(int subCategoryId, double price)
        {
            SubCategoryId = subCategoryId;
            Price = price;
            Opinions = new List<Opinion>();
        }

        public int ContractorId { get; set; }
        public int SubCategoryId { get; set; }
        public double Price { get; set; }
        public Subcategory SubCategory { get; set; }
        public Contractor Contractor { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Opinion> Opinions { get; set; }
    }
}