using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.UnitOfWork;

namespace BLL.Services.Realizations
{
    public class BookService : IBookService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IMapper _rmapper;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(x => x.CreateMap<Book, BookDTO>()).CreateMapper();
            _rmapper = new MapperConfiguration(x => x.CreateMap<BookDTO, Book>()).CreateMapper();
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(await _unitOfWork.Book.GetAllAsync());
        }

        public async Task<BookDTO> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.Book.GetByIdAsync(id);

            if (book == null)
                throw new ResultException("Db query result to books is null");

            return _mapper.Map<Book, BookDTO>(book);
        }

        public IEnumerable<BookDTO> GetBooksByGenreId(int genreId)
        {
            var book = _unitOfWork.Book.GetAll().Where(x => x.GenreId == genreId);

            if (book == null)
                throw new ResultException("Db query result to books is null");

            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(book);
        }

        public IEnumerable<BookDTO> GetBooksByAuthorId(int authorId)
        {
            var book = _unitOfWork.Book.GetAll().Where(x => x.AuthorId == authorId);

            if (book == null)
                throw new ResultException("Db query result to books is null");

            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(book);
        }

        public async Task<BookDTO> AddAsync(BookDTO bookDto)
        {
            var book = _rmapper.Map<BookDTO, Book>(bookDto);

            await _unitOfWork.Book.AddAsync(book);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to books weren't produced");

            return _mapper.Map<Book, BookDTO>(book);
        }

        public void Update(BookDTO bookDto)
        {
            var book = _unitOfWork.Book.GetByIdAsync(bookDto.Id).Result;

            if (book == null)
                throw new ResultException("There isn't such book in db");

            book = _rmapper.Map<BookDTO, Book>(bookDto);
            
            _unitOfWork.Book.Update(book);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to books weren't produced");
        }

        public void Remove(int id)
        {
            var book = _unitOfWork.Book.GetByIdAsync(id).Result;

            if (book == null)
                throw new ResultException("No record to remove from books");

            _unitOfWork.Book.Remove(book);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to books weren't produced");
        }
    }
}