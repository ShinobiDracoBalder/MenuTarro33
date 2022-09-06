using AutoMapper;
using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Responses;
using MenuTarro33.Common.Utilities;
using MenuTarro33.Web.Helpers;
using MenuTarro33.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;
using static MenuTarro33.Web.Helpers.ModalHelper;
using static NuGet.Packaging.PackagingConstants;

namespace MenuTarro33.Web.Controllers
{
    public class PlatillosController : Controller
    {
        private readonly IPlatilloRepository _platilloRepository;
        private readonly IMapper _mapper;
        private readonly ICombosHelper _combosHelper;
        private readonly IFlashMessage _flashMessage;
        private readonly IImageVideoHelper _imageVideoHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlatillosController(IPlatilloRepository platilloRepository, IMapper mapper,
            ICombosHelper combosHelper, IFlashMessage flashMessage, 
            IImageVideoHelper imageVideoHelper, IConverterHelper converterHelper, IWebHostEnvironment webHostEnvironment)
        {
            _platilloRepository = platilloRepository;
            _mapper = mapper;
            _combosHelper = combosHelper;
            _flashMessage = flashMessage;
            _imageVideoHelper = imageVideoHelper;
            _converterHelper = converterHelper;
            _webHostEnvironment = webHostEnvironment;
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
                var PlatilloV = await _converterHelper.ToPlatilloAsync(model, true);
                Guid imageId = Guid.Empty;
                string path = string.Empty;
                if (model.ImageFile != null)
                {
                    //imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                    path = await _imageVideoHelper.UploadImageAsync(model.ImageFile, "ImagePlatillos");
                }
                PlatilloV.ImagePath = path;
                var response = await _platilloRepository.AddAsync(PlatilloV);
                if (!response.IsSuccess)
                {
                    _flashMessage.Danger($"Error(In) en el Registro.  {response.Message}");
                    model.Categories = await _combosHelper.GetComboCategoriesAsync();
                    return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", model) });
                }

                var _Respponse = await _platilloRepository.GetAllTblPlatillosAsync();

                if (!_Respponse.IsSuccess)
                {
                    _flashMessage.Danger($"Error(In) en el Registro.  {_Respponse.Message}");
                    model.Categories = await _combosHelper.GetComboCategoriesAsync();
                    return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", model) });
                }

                return Json(new
                {
                    isValid = true,
                    html = ModalHelper.RenderRazorViewToString(this, "_ViewAllPlatillo", _Respponse.ListResults.ToList())
                });
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
                ImagePath = Platillo.Result.ImagePath,
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

                var PlatilloV = await _converterHelper.ToPlatilloUPAsync(model, false);
                Guid imageId = Guid.Empty;
                string path = model.ImagePath;
                
                // Eliminar la imagen
                string upload = _webHostEnvironment.WebRootPath + WC.ImagenPlatilloRuta;
                string uploadVideo = _webHostEnvironment.WebRootPath + WC.VideoRuta;
                if (model.ImageFile != null)
                {
                    // borrar la imagen anterior
                    var anteriorFile = Path.Combine(upload, model.ImagePath.Substring(24));
                    if (System.IO.File.Exists(anteriorFile))
                    {
                        System.IO.File.Delete(anteriorFile);
                    }
                    //imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                    path = await _imageVideoHelper.UploadImageAsync(model.ImageFile, "ImagePlatillos");
                }
                PlatilloV.ImagePath = path;
                var response = await _platilloRepository.UpdateAsync(PlatilloV);
                if (!response.IsSuccess)
                {
                    _flashMessage.Danger($"Error(In) en el Registro.  {response.Message}");
                    model.Categories = await _combosHelper.GetComboCategoriesAsync();
                    return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", model) });
                }

                var _Respponse = await _platilloRepository.GetAllTblPlatillosAsync();

                if (!_Respponse.IsSuccess)
                {
                    _flashMessage.Danger($"Error(In) en el Registro.  {_Respponse.Message}");
                    model.Categories = await _combosHelper.GetComboCategoriesAsync();
                    return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", model) });
                }

                return Json(new
                {
                    isValid = true,
                    html = ModalHelper.RenderRazorViewToString(this, "_ViewAllPlatillo", _Respponse.ListResults.ToList())
                });
            }
            model.Categories = await _combosHelper.GetComboCategoriesAsync();
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", model) });
        }
    }
}
