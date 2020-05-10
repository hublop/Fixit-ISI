using System;

namespace Fixit.Application.Contractors.Queries.GetProfile
{
    public class OpinionsInProfile
    {
        public int Id { get; set; }
        public RatingInProfile Rating { get; set; }
        public string Comment { get; set; }
        public CustomerInProfile Customer { get; set; }
        public DateTime CreatedOn { get; set; }
        public SubcategoryInProfile Subcategory { get; set; }
    }
}