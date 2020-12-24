using DAL.Contexts;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Realizations
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
    }
}