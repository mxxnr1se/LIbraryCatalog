using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                    new Genre {Id = 1, Name = "Poetry"},
                    new Genre {Id = 2, Name = "Fiction"},
                    new Genre {Id = 3, Name = "Mystery"},
                    new Genre {Id = 4, Name = "Detective"},
                    new Genre {Id = 5, Name = "Romance"}
            );
        }
    }
}