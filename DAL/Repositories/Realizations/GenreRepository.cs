using DAL.Contexts;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Realizations
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(LibraryContext context) : base(context)
        {
        }
    }
}