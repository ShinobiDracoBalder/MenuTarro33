using AutoMapper;
using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Common.DataBase;
using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Responses;
using Microsoft.EntityFrameworkCore;
using System;

namespace MenuTarro33.Common.Application.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CategoriaRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
           _applicationDbContext = applicationDbContext;
           _mapper = mapper;
        }

        public async Task<GenericResponse<TbCategoria>> AddAsync(TbCategoria entity)
        {
            try
            {
                _applicationDbContext.TbCategoria.Add(entity);
               await _applicationDbContext.SaveChangesAsync();
                return new GenericResponse<TbCategoria>
                {
                    IsSuccess = true,
                };
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return new GenericResponse<TbCategoria>
                    {
                        IsSuccess = false,
                        Message = "Ya existe un categoria con el mismo nombre."
                    };
                }
                else
                {
                    return new GenericResponse<TbCategoria>
                    {
                        IsSuccess = false,
                        Message = dbUpdateException.InnerException.Message
                    };
                }
            }
            catch (Exception exception)
            {
                return new GenericResponse<TbCategoria>
                {
                    IsSuccess = false,
                    Message = exception.InnerException.Message
                };
            }
        }
        public async Task<GenericResponse<TbCategoria>> DeleteAsync(int id)
        {
            try
            {
                return new GenericResponse<TbCategoria>
                {
                    IsSuccess = true,
                };
            }
            catch (Exception exception)
            {
                return new GenericResponse<TbCategoria>
                {
                    IsSuccess = false,
                    Message = exception.InnerException.Message
                };
            }
        }

        public async Task<GenericResponse<TbCategoria>> GetAllAsync()
        {
            try
            {
                List<TbCategoria> listAll = await _applicationDbContext
                .TbCategoria.Where(c => c.Activo == 1)
                .ToListAsync();
               
                return new GenericResponse<TbCategoria> {
                    IsSuccess = true,
                    ListResults = listAll,
                };
            }
            catch (Exception exception)
            {
                return new GenericResponse<TbCategoria> {
                 IsSuccess = false,
                 Message = exception.InnerException.Message
                };
            }
        }

        public async Task<GenericResponse<CategoriaDto>> GetAllTblCategoriasAsync()
        {
            try
            {
                List<TbCategoria> listAll = await _applicationDbContext
                .TbCategoria.Where(c => c.Activo == 1)
                .ToListAsync();

                var ListDto = new List<CategoriaDto>();

                foreach (var list in listAll)
                {
                    ListDto.Add(_mapper.Map<CategoriaDto>(list));
                }

                return new GenericResponse<CategoriaDto>
                {
                    IsSuccess = true,
                    ListResults = ListDto,
                };
            }
            catch (Exception exception)
            {
                return new GenericResponse<CategoriaDto>
                {
                    IsSuccess = false,
                    Message = exception.InnerException.Message
                };
            }
        }

        public async Task<GenericResponse<TbCategoria>> GetByIdAsync(int id)
        {
            try
            {
                return new GenericResponse<TbCategoria>
                {
                    IsSuccess = true,
                };
            }
            catch (Exception exception)
            {
                return new GenericResponse<TbCategoria>
                {
                    IsSuccess = false,
                    Message = exception.InnerException.Message
                };
            }
        }

        public Task<GenericResponse<CategoriaDto>> GetOnlyTblCategoriaAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<GenericResponse<TbCategoria>> UpdateAsync(TbCategoria entity)
        {
            try
            {
                var categoria = await _applicationDbContext
                    .TbCategoria.FirstOrDefaultAsync(c=> c.CategoriaId == entity.CategoriaId);

                if (categoria == null) 
                {
                    return new GenericResponse<TbCategoria>
                    {
                        IsSuccess = false,
                        Message = "No existe un categoria con el mismo nombre."
                    };
                }
                categoria.NombreCategoria = entity.NombreCategoria ?? categoria.NombreCategoria;
                categoria.Descripcion = entity.Descripcion ?? categoria.Descripcion;
                categoria.ImagePath = entity.ImagePath ?? categoria.ImagePath;
                categoria.VideoPath = entity.VideoPath ?? categoria.VideoPath;

                _applicationDbContext.TbCategoria.Update(categoria);
                await _applicationDbContext.SaveChangesAsync(); 
                return new GenericResponse<TbCategoria> {
                    IsSuccess = true,
                }; 
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return new GenericResponse<TbCategoria>
                    {
                        IsSuccess = false,
                        Message = "Ya existe un categoria con el mismo nombre."
                    };
                }
                else
                {
                    return new GenericResponse<TbCategoria>
                    {
                        IsSuccess = false,
                        Message = dbUpdateException.InnerException.Message
                    };
                }
            }
            catch (Exception exception)
            {
                return new GenericResponse<TbCategoria>
                {
                    IsSuccess = false,
                    Message = exception.InnerException.Message
                };
            }
        }
    }
}
