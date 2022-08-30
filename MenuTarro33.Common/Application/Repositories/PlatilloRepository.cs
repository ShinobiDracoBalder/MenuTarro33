using AutoMapper;
using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Common.DataBase;
using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Responses;
using Microsoft.EntityFrameworkCore;

namespace MenuTarro33.Common.Application.Repositories
{
    public class PlatilloRepository : IPlatilloRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public PlatilloRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<GenericResponse<TbPlatillo>> AddAsync(TbPlatillo entity)
        {
            try
            {
                _applicationDbContext.Platillos.Add(entity);
                await _applicationDbContext.SaveChangesAsync();
                return new GenericResponse<TbPlatillo>
                {
                    IsSuccess = true,
                };
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return new GenericResponse<TbPlatillo>
                    {
                        IsSuccess = false,
                        Message = "Ya existe un Platillo con el mismo nombre."
                    };
                }
                else
                {
                    return new GenericResponse<TbPlatillo>
                    {
                        IsSuccess = false,
                        Message = dbUpdateException.InnerException.Message
                    };
                }
            }
            catch (Exception exception)
            {
                return new GenericResponse<TbPlatillo>
                {
                    IsSuccess = false,
                    Message = exception.InnerException.Message
                };
            }
        }

        public Task<GenericResponse<TbPlatillo>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<TbPlatillo>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<PlatilloDto>> GetAllTblPlatillosAsync()
        {
            try
            {
                List<TbPlatillo> listAll = await _applicationDbContext
                .Platillos.Where(c => c.Activo == 1)
                .ToListAsync();

                var ListDto = new List<PlatilloDto>();

                foreach (var list in listAll)
                {
                    ListDto.Add(_mapper.Map<PlatilloDto>(list));
                }

                return new GenericResponse<PlatilloDto>
                {
                    IsSuccess = true,
                    ListResults = ListDto,
                };
            }
            catch (Exception exception)
            {
                return new GenericResponse<PlatilloDto>
                {
                    IsSuccess = false,
                    Message = exception.InnerException.Message
                };
            }
        }

        public Task<GenericResponse<TbPlatillo>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<PlatilloDto>> GetOnlyTblPlatilloAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<GenericResponse<TbPlatillo>> UpdateAsync(TbPlatillo entity)
        {
            try
            {
                _applicationDbContext.Platillos.Update(entity);
                await _applicationDbContext.SaveChangesAsync();
                return new GenericResponse<TbPlatillo>
                {
                    IsSuccess = true,
                };
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return new GenericResponse<TbPlatillo>
                    {
                        IsSuccess = false,
                        Message = "Ya existe un platillo con el mismo nombre."
                    };
                }
                else
                {
                    return new GenericResponse<TbPlatillo>
                    {
                        IsSuccess = false,
                        Message = dbUpdateException.InnerException.Message
                    };
                }
            }
            catch (Exception exception)
            {
                return new GenericResponse<TbPlatillo>
                {
                    IsSuccess = false,
                    Message = exception.InnerException.Message
                };
            }
        }
    }
}
