using AutoMapper;
using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;

namespace MenuTarro33.Common.SExplMappers
{
    public class SpExplorationMapper : Profile
    {
        public SpExplorationMapper()
        {
            CreateMap<TbCategoria, CategoriaDto>().ReverseMap();
            CreateMap<TbPlatillo, PlatilloDto>().ReverseMap();
        }
    }
}
