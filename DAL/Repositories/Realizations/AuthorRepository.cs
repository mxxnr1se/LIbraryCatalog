using System.Linq;
using DAL.Contexts;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Realizations
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context) : base(context)
        {
        }
    }
}