using System.Collections.Generic;
using BLL.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IFormService : IService<FormDTO>
    {
        IEnumerable<FormDTO> GetReaderBooks(int readerId);

        IEnumerable<FormDTO> GetBookReaders(int bookId);

        void RemoveBook(int readerId, int bookId);
    }
}