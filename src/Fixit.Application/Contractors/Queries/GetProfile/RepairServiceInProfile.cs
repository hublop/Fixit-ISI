using Fixit.Domain.Entities;

namespace Fixit.Application.Contractors.Queries.GetProfile
{
    public class RepairServiceInProfile
    {
        public int ContractorId { get; set; }
        public int SubCategoryId { get; set; }
        public double Price { get; set; }
        public SubcategoryInProfile SubCategory { get; set; }
        public Contractor Contractor { get; set; }
    }
}