using System.Collections.Generic;
using BLL.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IBookService : IService<BookDTO>
    {
        IEnumerable<BookDTO> GetBooksByGenreId(int genreId);

        IEnumerable<BookDTO> GetBooksByAuthorId(int authorId);
    }
}