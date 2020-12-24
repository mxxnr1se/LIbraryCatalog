using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.LastName).IsRequired();

            builder.HasData(
                    new Author {Id = 1, FirstName = "William", LastName = "Shakespeare"},
                    new Author {Id = 2, FirstName = "Agatha", LastName = "Christie"},
                    new Author {Id = 3, FirstName = "Barbara", LastName = "Cartland"},
                    new Author {Id = 4, FirstName = "Kyotaro", LastName = "Nishimura"},
                    new Author {Id = 5, FirstName = "Danielle", LastName = "Steel"},
                    new Author {Id = 6, FirstName = "Georges", LastName = "Simenon"},
                    new Author {Id = 7, FirstName = "Corín", LastName = "Tellado"},
                    new Author {Id = 8, FirstName = "Alexander", LastName = "Pushkin"},
                    new Author {Id = 9, FirstName = "Gilbert", LastName = "Patten"},
                    new Author {Id = 10, FirstName = "Gilbert", LastName = "Patten"}
            );
        }
    }
}