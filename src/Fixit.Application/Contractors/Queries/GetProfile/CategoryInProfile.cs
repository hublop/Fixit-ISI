using System.Collections.Generic;

namespace Fixit.Application.Contractors.Queries.GetProfile
{
    public class CategoryInProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<SubcategoryInProfile> Subcategories { get; set; }
    }
}