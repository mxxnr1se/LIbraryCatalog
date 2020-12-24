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
    public class ReaderService : IReaderService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IMapper _rmapper;

        public ReaderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(x => x.CreateMap<Reader, ReaderDTO>()).CreateMapper();
            _rmapper = new MapperConfiguration(x => x.CreateMap<ReaderDTO, Reader>()).CreateMapper();
        }

        public async Task<IEnumerable<ReaderDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Reader>, IEnumerable<ReaderDTO>>(await _unitOfWork.Reader.GetAllAsync());
        }

        public async Task<ReaderDTO> GetByIdAsync(int id)
        {
            var reader = await _unitOfWork.Reader.GetByIdAsync(id);

            if (reader == null)
                throw new ResultException("Db query result to readers is null");

            return _mapper.Map<Reader, ReaderDTO>(reader);
        }

        public async Task<ReaderDTO> AddAsync(ReaderDTO readerDto)
        {
            var reader = _rmapper.Map<ReaderDTO, Reader>(readerDto);

            await _unitOfWork.Reader.AddAsync(reader);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to readers weren't produced");

            return _mapper.Map<Reader, ReaderDTO>(reader);
        }

        public void Update(ReaderDTO readerDto)
        {
            var reader = _unitOfWork.Reader.GetByIdAsync(readerDto.Id).Result;

            if (reader == null)
                throw new ResultException("There isn't such reader in db");

            reader = _rmapper.Map<ReaderDTO, Reader>(readerDto);

            _unitOfWork.Reader.Update(reader);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to readers weren't produced");
        }

        public void Remove(int id)
        {
            var reader = _unitOfWork.Reader.GetByIdAsync(id).Result;

            if (reader == null)
                throw new ResultException("No record to remove from readers");

            _unitOfWork.Reader.Remove(reader);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to readers weren't produced");
        }
    }
}