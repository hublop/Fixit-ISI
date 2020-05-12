using System;
using System.Collections.Generic;
using System.Linq;
using Fixit.Domain.Common;

namespace Fixit.Domain.Entities
{
    public class Contractor : User
    {
        public string CompanyName { get; set; }
        public string SelfDescription { get; set; }
        public DateTime ContractorFrom { get; set; }
        private List<RepairService> _repairServices = new List<RepairService>();
        public IReadOnlyCollection<RepairService> RepairServices => _repairServices.AsReadOnly();
        public int? LocationId { get; set; }
        public Location Location { get; set; }
        //todo: to delete
        public bool IsPremium { get; set; }
        public ICollection<OrderOffer> OrderOffers { get; set; }

        public string ContractorUUID { get; set; }

        public int? SubscriptionStatusId { get; set; }
        public SubscriptionStatus SubscriptionStatus { get; set; }
        public DateTime? NextPaymentDate { get; set; }

    public void ProvideRepairService(int subcategoryId, double price)
        {
            if (ProvidesRepairService(subcategoryId))
            {
                throw new DomainException($"Repair service exists ({Id} - {subcategoryId})", "Order Exists.");
            }

            _repairServices.Add(new RepairService(subcategoryId, price));
        }

        public void UnprovideRepairService(int subcategoryId)
        {
            if (!ProvidesRepairService(subcategoryId))
            {
                throw new DomainException($"Repair service not provided: {subcategoryId}", "Repair service does not exist.");
            }
            var toUnprovide = _repairServices.First(x => x.SubCategoryId == subcategoryId);
            _repairServices.Remove(toUnprovide);

            // _opinions.RemoveAll(x => x.SubcategoryId == subcategoryId);
        }

        public void AddOpinion(string comment, int subcategoryId, double punctuality, double involvement, double quality)
        {
            if (!ProvidesRepairService(subcategoryId))
            {
                throw new DomainException($"Repair service not provided: {subcategoryId}", "Repair service does not exist.");
            }

            var repairService = _repairServices.First(x => x.SubCategoryId == subcategoryId);
            repairService.Opinions.Add(new Opinion(comment, subcategoryId, punctuality, involvement, quality));
        }

        public bool ProvidesRepairService(int subcategoryId)
        {
            return RepairServices.Any(x => x.SubCategoryId == subcategoryId);
        }

        public double GetPunctualityAverage()
        {
            var opinions = RepairServices.SelectMany(x => x.Opinions).ToList();
            return opinions.Any() ? opinions.Average(x => x.Rating.Punctuality) : 0;
        }

        public double GetInvolvementAverage()
        {
            var opinions = RepairServices.SelectMany(x => x.Opinions).ToList();
            return opinions.Any() ? opinions.Average(x => x.Rating.Involvement) : 0;
        }

        public double GetQualityAverage()
        {
            var opinions = RepairServices.SelectMany(x => x.Opinions).ToList();
            return opinions.Any() ? opinions.Average(x => x.Rating.Quality) : 0;
        }

        public double GetAvgRating()
        {
            return new List<double>()
            {
                GetPunctualityAverage(),
                GetInvolvementAverage(),
                GetQualityAverage()
            }.Average();
        }
    }
}