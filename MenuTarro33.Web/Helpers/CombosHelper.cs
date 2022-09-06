using MenuTarro33.Common.DataBase;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MenuTarro33.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CombosHelper(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync()
        {
            List<SelectListItem> list = await _applicationDbContext
                .TbCategoria.Select(c => new SelectListItem
                {
                    Text = c.NombreCategoria,
                    Value = c.CategoriaId.ToString()
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Seleccione una categoría...", Value = "0" });
            return list;
        }
    }
}
