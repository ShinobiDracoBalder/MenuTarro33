using MenuTarro33.Common.Responses;

namespace MenuTarro33.Common.Application.Interfaces
{
    public interface IGenericRepositoryFactory<T> where T : class
    {
        Task<GenericResponse<T>> GetByIdAsync(int id);
        Task<GenericResponse<T>> GetAllAsync();
        Task<GenericResponse<T>> AddAsync(T entity);
        Task<GenericResponse<T>> UpdateAsync(T entity);
        Task<GenericResponse<T>> DeleteAsync(int id);
        Task<bool> SaveAllAsync();
    }
}
