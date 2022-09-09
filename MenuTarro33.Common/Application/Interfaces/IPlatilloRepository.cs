using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Responses;
using System.Linq.Expressions;

namespace MenuTarro33.Common.Application.Interfaces
{
    public interface IPlatilloRepository : IGenericRepositoryFactory<TbPlatillo>
    {
        Task<GenericResponse<PlatilloDto>> GetAllTblPlatillosAsync();
        Task<GenericResponse<PlatilloDto>> GetOnlyTblPlatilloAsync(int id);
        IQueryable<TbPlatillo> GetFullAsync(int id);
        Task<GenericResponse<TbPlatillo>> GetAllTblPlatilloAsync(int id);
    }
}
