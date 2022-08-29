using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Common.DataBase;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Responses;

namespace MenuTarro33.Common.Application.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoriaRepository(ApplicationDbContext applicationDbContext)
        {
           _applicationDbContext = applicationDbContext;
        }

        public Task<GenericResponse<TbCategoria>> AddAsync(TbCategoria entity)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<TbCategoria>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<TbCategoria>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<TbCategoria>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public Task<GenericResponse<TbCategoria>> UpdateAsync(TbCategoria entity)
        {
            throw new NotImplementedException();
        }
    }
}
