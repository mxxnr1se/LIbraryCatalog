using DAL.Contexts;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Realizations
{
    public class ReaderRepository : Repository<Reader>, IReaderRepository
    {
        public ReaderRepository(LibraryContext context) : base(context)
        {
        }
    }
}