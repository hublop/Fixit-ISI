using System;
using System.Collections.Generic;
using System.Text;
using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
  public class SubscriptionStatusEntityTypeConfiguration: IEntityTypeConfiguration<SubscriptionStatus>
  {
    public void Configure(EntityTypeBuilder<SubscriptionStatus> builder)
    {
      builder.ToTable("SubscriptionStatus");

      builder.Property(x => x.Id)
        .HasColumnName("SubscriptionStatusId")
        .UseHiLo("SubscriptionStatusSequence")
        .ValueGeneratedOnAdd()
        .IsRequired();

      builder.HasKey(x => x.Id);

      builder.HasMany(x => x.Contractors)
        .WithOne(x => x.SubscriptionStatus)
        .HasForeignKey(x => x.SubscriptionStatusId);


    }
  }
}


