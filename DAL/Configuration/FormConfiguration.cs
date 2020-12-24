using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.HasData(
                    new Form {Id = 1, ReaderId = 1, BookId = 2},
                    new Form {Id = 2, ReaderId = 1, BookId = 3},
                    new Form {Id = 3, ReaderId = 1, BookId = 6},
                    new Form {Id = 4, ReaderId = 1, BookId = 1},
                    new Form {Id = 5, ReaderId = 1, BookId = 3},
                    new Form {Id = 6, ReaderId = 1, BookId = 4},
                    new Form {Id = 7, ReaderId = 2, BookId = 20},
                    new Form {Id = 8, ReaderId = 2, BookId = 11},
                    new Form {Id = 9, ReaderId = 3, BookId = 5},
                    new Form {Id = 10, ReaderId = 3, BookId = 5},
                    new Form {Id = 11, ReaderId = 3, BookId = 2},
                    new Form {Id = 12, ReaderId = 3, BookId = 7},
                    new Form {Id = 13, ReaderId = 3, BookId = 9},
                    new Form {Id = 14, ReaderId = 3, BookId = 10},
                    new Form {Id = 15, ReaderId = 3, BookId = 11},
                    new Form {Id = 16, ReaderId = 3, BookId = 12},
                    new Form {Id = 17, ReaderId = 3, BookId = 13},
                    new Form {Id = 18, ReaderId = 3, BookId = 14},
                    new Form {Id = 19, ReaderId = 4, BookId = 4},
                    new Form {Id = 20, ReaderId = 4, BookId = 8},
                    new Form {Id = 21, ReaderId = 4, BookId = 15},
                    new Form {Id = 22, ReaderId = 4, BookId = 17},
                    new Form {Id = 23, ReaderId = 5, BookId = 16},
                    new Form {Id = 24, ReaderId = 5, BookId = 17},
                    new Form {Id = 25, ReaderId = 5, BookId = 18},
                    new Form {Id = 26, ReaderId = 5, BookId = 19},
                    new Form {Id = 27, ReaderId = 5, BookId = 20},
                    new Form {Id = 28, ReaderId = 5, BookId = 17},
                    new Form {Id = 29, ReaderId = 6, BookId = 1},
                    new Form {Id = 30, ReaderId = 6, BookId = 4},
                    new Form {Id = 31, ReaderId = 6, BookId = 9},
                    new Form {Id = 32, ReaderId = 6, BookId = 7},
                    new Form {Id = 33, ReaderId = 6, BookId = 2},
                    new Form {Id = 34, ReaderId = 6, BookId = 6}
            );
        }
    }
}