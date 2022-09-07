using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Common.Application.Repositories;
using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Utilities;
using MenuTarro33.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MenuTarro33.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IPlatilloRepository _platilloRepository;

        public HomeController(ILogger<HomeController> logger, ICategoriaRepository categoriaRepository,IPlatilloRepository platilloRepository)
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
            _platilloRepository = platilloRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ListResponse = await _categoriaRepository.GetAllTblCategoriasAsync();
            return View(ListResponse.ListResults);
        }

        public async Task<IActionResult> MasterMenu(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "PriceDesc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var ListResponse = await _categoriaRepository.GetAllTblCategoriasAsync();
            return View(ListResponse.ListResults);
        }

        public async Task<IActionResult> PlatillosMenu(int? id, string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "PriceDesc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var ListResult = await _platilloRepository.GetAllTblPlatilloAsync(id.Value);

            IQueryable<TbPlatillo> query = ListResult.SpecialResults;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query
                    .Where(p => (p.NombrePlatillo.ToLower().Contains(searchString.ToLower())));
            }
            switch (sortOrder)
            {
                case "NameDesc":
                    query = query.OrderByDescending(p => p.NombrePlatillo);
                    break;
                case "Price":
                    query = query.OrderBy(p => p.Precio);
                    break;
                case "PriceDesc":
                    query = query.OrderByDescending(p => p.Precio);
                    break;
                default:
                    query = query.OrderBy(p => p.NombrePlatillo);
                    break;
            }
            int pageSize = 8;
            HomeViewModel model = new()
            {
                 Platillos = await PaginatedList<TbPlatillo>.CreateAsync(query, pageNumber ?? 1, pageSize),
            };
            return View(model);
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