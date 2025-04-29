using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistants.Data.configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, address => address.WithOwner());

            builder.HasMany(O => O.OrderItems)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.DeliveryMethod)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

            builder.Property(O => O.PaymentStatus)
.HasConversion(s => s.ToString(), s => Enum.Parse<OrderPaymentStatus>(s));

            builder.Property(O => O.SubTotal)
            .HasColumnType(typeName: "decimal(18,4)");
        }
    }
}
