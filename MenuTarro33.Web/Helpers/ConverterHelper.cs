using MenuTarro33.Common.DataBase;
using MenuTarro33.Common.Entities;
using MenuTarro33.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuTarro33.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ConverterHelper(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<TbPlatillo> ToPlatilloAsync(PlatilloViewModel model, bool isNew)
        {
            var categoria = await _applicationDbContext.TbCategoria.FirstOrDefaultAsync(x => x.CategoriaId.Equals(model.CategoriaId));

            return new TbPlatillo
            {
                FechaRegistro = DateTime.Now.ToUniversalTime(),
                Activo = 1,  
                Descripcion = model.Descripcion,
                PlatilloId = isNew ? 0 : model.PlatilloId,
                CategoriaId = categoria.CategoriaId,
                NombrePlatillo = model.NombrePlatillo,
                Precio = model.Precio,  
            };
        }

        public async Task<TbPlatillo> ToPlatilloUPAsync(PlatilloViewModel model, bool isNew)
        {
            var platillo = await _applicationDbContext
                .TbPlatillo.FirstOrDefaultAsync(x => x.PlatilloId.Equals(model.PlatilloId));
            //return new TbPlatillo
            //{
            //    FechaRegistro = DateTime.Now.ToUniversalTime(),
            //    Activo = 1,
            //    Descripcion = platillo.Descripcion ?? model.Descripcion,
            //    PlatilloId = isNew ? 0 : model.PlatilloId,
            //    CategoriaId = model.CategoriaId,
            //    NombrePlatillo = platillo.NombrePlatillo ?? model.NombrePlatillo,
            //    Precio = model.Precio,
            //};
            platillo.ImagePath = model.ImagePath ?? platillo.ImagePath;
            platillo.CategoriaId = model.CategoriaId;
            platillo.NombrePlatillo = model.NombrePlatillo ?? platillo.NombrePlatillo;
            platillo.Precio = model.Precio;
            platillo.Descripcion = model.Descripcion ?? platillo.Descripcion;
            
            return platillo;
        }
    }
}
