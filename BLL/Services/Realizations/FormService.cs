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
    public class FormService : IFormService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IMapper _rmapper;

        public FormService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(x => x.CreateMap<Form, FormDTO>()).CreateMapper();
            _rmapper = new MapperConfiguration(x => x.CreateMap<FormDTO, Form>()).CreateMapper();
        }

        public async Task<IEnumerable<FormDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Form>, IEnumerable<FormDTO>>(await _unitOfWork.Form.GetAllAsync());
        }

        public async Task<FormDTO> GetByIdAsync(int id)
        {
            var form = await _unitOfWork.Form.GetByIdAsync(id);

            if (form == null)
                throw new ResultException("Db query result to forms is null");

            return _mapper.Map<Form, FormDTO>(form);
        }

        public IEnumerable<FormDTO> GetReaderBooks(int readerId)
        {
            var form = _unitOfWork.Form.GetAll().Where(x => x.ReaderId == readerId);

            if (form == null)
                throw new ResultException("Db query result to forms is null");

            return _mapper.Map<IEnumerable<Form>, IEnumerable<FormDTO>>(form);
        }

        public IEnumerable<FormDTO> GetBookReaders(int bookId)
        {
            var form = _unitOfWork.Form.GetAll().Where(x => x.BookId == bookId);

            if (form == null)
                throw new ResultException("Db query result to forms is null");

            return _mapper.Map<IEnumerable<Form>, IEnumerable<FormDTO>>(form);
        }

        public async Task<FormDTO> AddAsync(FormDTO formDto)
        {
            var form = _rmapper.Map<FormDTO, Form>(formDto);

            await _unitOfWork.Form.AddAsync(form);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to forms weren't produced");

            return _mapper.Map<Form, FormDTO>(form);
        }

        public void Update(FormDTO formDto)
        {
            var form = _unitOfWork.Form.GetByIdAsync(formDto.Id).Result;

            if (form == null)
                throw new ResultException("There isn't such form in db");

            form = _rmapper.Map<FormDTO, Form>(formDto);

            _unitOfWork.Form.Update(form);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to forms weren't produced");
        }

        public void RemoveBook(int readerId, int bookId)
        {
            var e = _unitOfWork.Form.GetAll().Where(x => x.ReaderId == readerId).Where(x => x.BookId == bookId);
            if (e.Any())
            {
                foreach (Form form in e)
                {
                    _unitOfWork.Form.Remove(form);
                }

                _unitOfWork.SaveChangesAsync();
            }
        }

        public void Remove(int id)
        {
            var form = _unitOfWork.Form.GetByIdAsync(id).Result;

            if (form == null)
                throw new ResultException("No record to remove from forms");

            _unitOfWork.Form.Remove(form);
            if (!_unitOfWork.SaveChangesAsync().Result)
                throw new ResultException("Changes to forms weren't produced");
        }
    }
}