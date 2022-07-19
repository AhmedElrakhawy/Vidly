using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vidly.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(string Url, int Id);
        Task<IEnumerable<T>> GetAll(string Url);
        Task<bool> CreateAsync(string Url, T ObjToCreate);
        Task<bool> DeleteAsync(string Url, int Id);
        Task<bool> UpdateAsync(string Url, T ObjToUpdate);
    }
}
