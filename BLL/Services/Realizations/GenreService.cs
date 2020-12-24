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
    public class GenreService : IGenreService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IMapper _rmapper;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(x => x.CreateMap<Genre, GenreDTO>()).CreateMapper();
            _rmapper = new MapperConfiguration(x => x.CreateMap<GenreDTO, Genre>()).CreateMapper();
        }

        public async Task<IEnumerable<GenreDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(await _unitOfWork.Genre.GetAllAsync());
        }

        public async Task<GenreDTO> GetByIdAsync(int id)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(id);

            if (genre == null)
                throw new ResultException("Db query result to genres is null");

            return _mapper.Map<Genre, GenreDTO>(genre);
        }

        public async Task<GenreDTO> AddAsync(GenreDTO genreDto)
        {
            var genre = _rmapper.Map<GenreDTO, Genre>(genreDto);

            await _unitOfWork.Genre.AddAsync(genre);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to genres weren't produced");

            return _mapper.Map<Genre, GenreDTO>(genre);
        }

        public void Update(GenreDTO genreDto)
        {
            var genre = _unitOfWork.Genre.GetByIdAsync(genreDto.Id).Result;

            if (genre == null)
                throw new ResultException("There isn't such genre in db");

            genre = _rmapper.Map<GenreDTO, Genre>(genreDto);

            _unitOfWork.Genre.Update(genre);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to genres weren't produced");
        }

        public void Remove(int id)
        {
            var genre = _unitOfWork.Genre.GetByIdAsync(id).Result;

            if (genre == null)
                throw new ResultException("No record to remove from genres");

            _unitOfWork.Genre.Remove(genre);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to genres weren't produced");
        }
    }
}