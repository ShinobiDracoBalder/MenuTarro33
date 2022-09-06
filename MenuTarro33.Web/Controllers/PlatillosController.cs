using AutoMapper;
using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Web.Helpers;
using MenuTarro33.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;
using static MenuTarro33.Web.Helpers.ModalHelper;

namespace MenuTarro33.Web.Controllers
{
    public class PlatillosController : Controller
    {
        private readonly IPlatilloRepository _platilloRepository;
        private readonly IMapper _mapper;
        private readonly ICombosHelper _combosHelper;
        private readonly IFlashMessage _flashMessage;

        public PlatillosController(IPlatilloRepository platilloRepository, IMapper mapper, ICombosHelper combosHelper, IFlashMessage flashMessage)
        {
            _platilloRepository = platilloRepository;
            _mapper = mapper;
            _combosHelper = combosHelper;
            _flashMessage = flashMessage;
        }
        public async Task<IActionResult> Index()
        {

            var ListResult = await _platilloRepository.GetAllTblPlatillosAsync();

            return View(ListResult.ListResults.ToList());
        }
        [NoDirectAccess]
        public async Task<IActionResult> Create()
        {
            PlatilloViewModel model = new()
            {
                Categories = await _combosHelper.GetComboCategoriesAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlatilloViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            model.Categories = await _combosHelper.GetComboCategoriesAsync();
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", model) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Edit(int id)
        {
            var Platillo = await _platilloRepository.GetByIdAsync(id);
            if (!Platillo.IsSuccess)
            {
                return NotFound();
            }

            PlatilloViewModel model = new()
            {
                NombrePlatillo = Platillo.Result.NombrePlatillo,
                PlatilloId   = Platillo.Result.PlatilloId,
                Descripcion = Platillo.Result.Descripcion,
                CategoriaId = Platillo.Result.CategoriaId,
                Precio = Platillo.Result.Precio,
                Categories = await _combosHelper.GetComboCategoriesAsync(),
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlatilloViewModel model)
        {
            if (id != model.PlatilloId)
            {
                _flashMessage.Danger($"Estos datos no son iguales data One {id}  data two {model.PlatilloId}.");
            }
            if (ModelState.IsValid) 
            {
            }
            model.Categories = await _combosHelper.GetComboCategoriesAsync();
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", model) });
        }
    }
}
