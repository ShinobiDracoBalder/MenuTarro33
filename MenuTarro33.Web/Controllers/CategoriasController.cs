using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Common.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MenuTarro33.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriasController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<IActionResult> Index()
        {
           var ListResponse = await _categoriaRepository.GetAllTblCategoriasAsync();
            return View(ListResponse.ListResults);
        }
    }
}
