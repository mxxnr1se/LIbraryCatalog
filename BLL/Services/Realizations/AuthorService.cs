using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.UnitOfWork;

namespace BLL.Services.Realizations
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IMapper _rmapper;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(x => x.CreateMap<Author, AuthorDTO>()).CreateMapper();
            _rmapper = new MapperConfiguration(x => x.CreateMap<AuthorDTO, Author>()).CreateMapper();
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDTO>>(await _unitOfWork.Author.GetAllAsync());
        }

        public async Task<AuthorDTO> GetByIdAsync(int id)
        {
            var author = await _unitOfWork.Author.GetByIdAsync(id);

            if (author == null)
                throw new ResultException("Db query result to authors is null");

            return _mapper.Map<Author, AuthorDTO>(author);
        }

        public async Task<AuthorDTO> AddAsync(AuthorDTO authorDto)
        {
            var author = _rmapper.Map<AuthorDTO, Author>(authorDto);

            await _unitOfWork.Author.AddAsync(author);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to authors weren't produced");

            return _mapper.Map<Author, AuthorDTO>(author);
        }

        public void Update(AuthorDTO authorDto)
        {
            var author = _unitOfWork.Author.GetByIdAsync(authorDto.Id).Result;

            if (author == null)
                throw new ResultException("There isn't such author in db");

            author = _rmapper.Map<AuthorDTO, Author>(authorDto);
            
            _unitOfWork.Author.Update(author);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to authors weren't produced");
        }

        public void Remove(int id)
        {
            var author = _unitOfWork.Author.GetByIdAsync(id).Result;

            if (author == null)
                throw new ResultException("No record to remove from authors");

            _unitOfWork.Author.Remove(author);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to authors weren't produced");
        }
    }
}