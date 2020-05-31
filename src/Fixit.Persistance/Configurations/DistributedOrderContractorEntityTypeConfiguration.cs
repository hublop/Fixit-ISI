using System;
using System.Collections.Generic;
using System.Text;
using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
  public class DistributedOrderContractorEntityTypeConfiguration: IEntityTypeConfiguration<DistributedOrderContractor>
  {
    public void Configure(EntityTypeBuilder<DistributedOrderContractor> builder)
    {
      builder.HasOne(x => x.Contractor)
        .WithMany(x => x.DistributedOrders)
        .HasForeignKey(x => x.ContractorId);

      builder.HasOne(x => x.Order)
        .WithMany(x => x.DistributedOrders)
        .HasForeignKey(x => x.OrderId);

      builder.HasKey(x => new
      {
        x.ContractorId,
        x.OrderId
      });
    }
  }
}
