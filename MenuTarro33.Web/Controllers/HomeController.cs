using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MenuTarro33.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoriaRepository _categoriaRepository;

        public HomeController(ILogger<HomeController> logger, ICategoriaRepository categoriaRepository)
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ListResponse = await _categoriaRepository.GetAllTblCategoriasAsync();
            return View(ListResponse.ListResults);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}