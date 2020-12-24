using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                    .HasOne(a => a.Author)
                    .WithMany()
                    .HasForeignKey(i => i.AuthorId)
                    .OnDelete(DeleteBehavior.SetNull);

            builder
                    .HasOne(a => a.Genre)
                    .WithMany()
                    .HasForeignKey(i => i.GenreId)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.Property(b => b.Title).IsRequired();

            builder.HasData(
                    new Book {Id = 1, Title = "Shakespeare's Sonnets", AuthorId = 1, GenreId = 1, Quantity = 5},
                    new Book {Id = 2, Title = "Shall I compare thee to a summer's day?", AuthorId = 1, GenreId = 1, Quantity = 2},
                    new Book {Id = 3, Title = "Hercule Poirot", AuthorId = 2, GenreId = 4, Quantity = 1},
                    new Book {Id = 4, Title = "Jane Marple", AuthorId = 2, GenreId = 4, Quantity = 6},
                    new Book {Id = 5, Title = "Duel of Hearts", AuthorId = 3, GenreId = 5, Quantity = 0},
                    new Book {Id = 6, Title = "Romantic Royal Marriages", AuthorId = 3, GenreId = 5, Quantity = 3},
                    new Book {Id = 7, Title = "A Ghost in Monte Carlo", AuthorId = 3, GenreId = 5, Quantity = 4},
                    new Book {Id = 8, Title = "Zoya", AuthorId = 5, GenreId = 5, Quantity = 3},
                    new Book {Id = 9, Title = "Sisters", AuthorId = 5, GenreId = 5, Quantity = 1},
                    new Book {Id = 10, Title = "The Mystery Train Disappears", AuthorId = 4, GenreId = 3, Quantity = 3},
                    new Book {Id = 11, Title = "Kisei Honsen Satsujin Jiken", AuthorId = 4, GenreId = 3, Quantity = 8},
                    new Book {Id = 12, Title = "Maigret", AuthorId = 6, GenreId = 4, Quantity = 3},
                    new Book {Id = 13, Title = "The Strange Case of Peter the Lett", AuthorId = 6, GenreId = 4, Quantity = 5},
                    new Book {Id = 14, Title = "Tu pasado me condena", AuthorId = 7, GenreId = 5, Quantity = 0},
                    new Book {Id = 15, Title = "Poltava", AuthorId = 8, GenreId = 1, Quantity = 2},
                    new Book {Id = 16, Title = "The Gypsies", AuthorId = 8, GenreId = 1, Quantity = 4},
                    new Book {Id = 17, Title = "Frank Merriwell", AuthorId = 9, GenreId = 2, Quantity = 6},
                    new Book {Id = 18, Title = "Boltwood of Yale", AuthorId = 9, GenreId = 2, Quantity = 1},
                    new Book {Id = 19, Title = "Winnetou", AuthorId = 10, GenreId = 2, Quantity = 5},
                    new Book {Id = 20, Title = "The Oil Prince", AuthorId = 10, GenreId = 2, Quantity = 3}
            );
        }
    }
}