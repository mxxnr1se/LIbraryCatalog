using System;
using System.Threading.Tasks;
using DAL.Contexts;
using DAL.Repositories.Interfaces;
using DAL.Repositories.Realizations;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryContext _dbContext;
        private IBookRepository _books;
        private IFormRepository _forms;
        private IReaderRepository _reader;
        private IGenreRepository _genre;
        private IAuthorRepository _author;

        public UnitOfWork(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBookRepository Book
        {
            get { return _books ?? (_books = new BookRepository(_dbContext)); }
        }

        public IFormRepository Form
        {
            get { return _forms ?? (_forms = new FormRepository(_dbContext)); }
        }

        public IReaderRepository Reader
        {
            get { return _reader ?? (_reader = new ReaderRepository(_dbContext)); }
        }

        public IGenreRepository Genre
        {
            get { return _genre ?? (_genre = new GenreRepository(_dbContext)); }
        }

        public IAuthorRepository Author
        {
            get { return _author ?? (_author = new AuthorRepository(_dbContext)); }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}