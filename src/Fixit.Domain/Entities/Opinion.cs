using System;

namespace Fixit.Domain.Entities
{
    public class Opinion : Entity
    {
        public Opinion()
        {

        }

        public Opinion(string comment, int subcategoryId, double punctuality, double involvement, double quality)
        {
            Rating = new Rating
            {
                Quality = quality,
                Involvement = involvement,
                Punctuality = punctuality
            };
            Comment = comment;
            SubcategoryId = subcategoryId;
            CreatedOn = DateTime.Now;
        }
        public Rating Rating { get; set; }
        public string Comment { get; set; }
        public int ContractorId { get; set; }
        public int SubcategoryId { get; set; }
        public DateTime CreatedOn { get; set; }
        public RepairService RepairService { get; set; }
    }
}