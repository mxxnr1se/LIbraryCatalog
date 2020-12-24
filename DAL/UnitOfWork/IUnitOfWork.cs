using System;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Book { get; }

        IFormRepository Form { get; }

        IReaderRepository Reader { get; }

        IAuthorRepository Author { get; }

        IGenreRepository Genre { get; }

        Task<bool> SaveChangesAsync();
    }
}