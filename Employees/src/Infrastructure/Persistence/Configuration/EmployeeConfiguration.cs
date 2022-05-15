using Employees.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.src.Infrastructure.Persistence.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.OwnsOne(p => p.Address)
                            .Property(p => p.Street).IsRequired();

            builder.OwnsOne(p => p.Address)
                            .Property(p => p.City).IsRequired();

            builder.OwnsOne(p => p.Address)
                            .Property(p => p.Country).IsRequired();
        }


    }
}
