using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Responses;

namespace MenuTarro33.Common.Application.Interfaces
{
    public interface IPlatilloRepository : IGenericRepositoryFactory<TbPlatillo>
    {
        Task<GenericResponse<PlatilloDto>> GetAllTblPlatillosAsync();
        Task<GenericResponse<PlatilloDto>> GetOnlyTblPlatilloAsync(int id);

        Task<GenericResponse<TbPlatillo>> GetAllTblPlatilloAsync(int id);
    }
}
