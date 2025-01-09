using Domain.Customers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            CustomerId => CustomerId.Value,
            value => new CustomerId(value));

        builder.Property(c => c.Name).HasMaxLength(50).IsRequired();

        builder.Property(c => c.LastName).HasMaxLength(50).IsRequired();

        builder.Ignore(c => c.FullName);

        builder.Property(c => c.Email).HasMaxLength(255).IsRequired();

        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.PhoneNumber).HasConversion(
            PhoneNumber => PhoneNumber.Value,
            value => PhoneNumber.Create(value)!)
            .HasMaxLength(10);

        builder.OwnsOne(c => c.Address, AddressBuilder =>
        {

            AddressBuilder.Property(a => a.Country).HasMaxLength(10);

            AddressBuilder.Property(a => a.Line1).HasMaxLength(20);

            AddressBuilder.Property(a => a.Line2).HasMaxLength(20);

            AddressBuilder.Property(a => a.City).HasMaxLength(40);

            AddressBuilder.Property(a => a.State).HasMaxLength(40);

            AddressBuilder.Property(a => a.ZipCode).HasMaxLength(10).IsRequired();
        });

        builder.Property(c => c.Active).IsRequired(true);
    }
}