using DAL.Contexts;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Realizations
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        public FormRepository(LibraryContext context) : base(context)
        {
        }
    }
}