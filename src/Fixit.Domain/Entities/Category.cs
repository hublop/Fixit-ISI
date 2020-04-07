using System;
using System.Collections.Generic;
using System.Linq;
using Fixit.Domain.Common;

namespace Fixit.Domain.Entities
{
    public class Category : Entity
    {
        public Category(string name, string description)
        {
            Name = name;
            Description = description;
            CreatedDate = DateTime.Now;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public IReadOnlyCollection<Subcategory> SubCategories => _subCategories.AsReadOnly();
        private List<Subcategory> _subCategories = new List<Subcategory>();


        public void AddSubCategory(Subcategory subCategory)
        {
            if (HasSubCategory(subCategory.Name))
            {
                throw new DomainException("Duplicate subcategory name.",
                    $"Subcategory with name {subCategory.Name} already exists.");
            }

            _subCategories.Add(subCategory);
        }

        private bool HasSubCategory(string name)
        {
            return SubCategories.Any(x => x.Name == name);
        }
    }
}