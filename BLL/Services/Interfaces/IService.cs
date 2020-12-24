using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IService<TDto> where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto> GetByIdAsync(int id);

        Task<TDto> AddAsync(TDto tDto);

        void Update(TDto tDto);

        void Remove(int id);
    }
}