using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class ReaderConfiguration : IEntityTypeConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.Property(r => r.LastName).IsRequired();

            builder.HasData(
                    new Reader
                    {
                            Id = 1, FirstName = "Catherine ", LastName = "McGinley", Phone = "719-557-7626",
                            DoB = new DateTime(1965, 9, 16)
                    },
                    new Reader
                    {
                            Id = 2, FirstName = "Jim", LastName = "Hyde", Phone = "517-663-4353",
                            DoB = new DateTime(1984, 12, 13)
                    },
                    new Reader
                    {
                            Id = 3, FirstName = "Merri", LastName = "Rosborough", Phone = "423-473-0603",
                            DoB = new DateTime(1976, 9, 25)
                    },
                    new Reader
                    {
                            Id = 4, FirstName = "Dale", LastName = "Pereira", Phone = "248-744-7065",
                            DoB = new DateTime(1969, 7, 23)
                    },
                    new Reader
                    {
                            Id = 5, FirstName = "John", LastName = "Larry", Phone = "208-530-9934",
                            DoB = new DateTime(1971, 2, 14)
                    },
                    new Reader
                    {
                            Id = 6, FirstName = "Richard", LastName = "Taylor", Phone = "913-403-7491",
                            DoB = new DateTime(1988, 7, 28)
                    }
            );
        }
    }
}