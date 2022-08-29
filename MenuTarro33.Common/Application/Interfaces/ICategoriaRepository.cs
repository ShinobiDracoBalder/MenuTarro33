using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Responses;

namespace MenuTarro33.Common.Application.Interfaces
{
    public interface ICategoriaRepository : IGenericRepositoryFactory<TbCategoria>
    {
        Task<GenericResponse<CategoriaDto>> GetAllTblCategoriasAsync();
        Task<GenericResponse<CategoriaDto>> GetOnlyTblCategoriaAsync(int id);
    }
}
