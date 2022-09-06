using AutoMapper;
using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Common.Application.Repositories;
using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Responses;
using MenuTarro33.Common.Utilities;
using MenuTarro33.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vereyon.Web;
using static MenuTarro33.Web.Helpers.ModalHelper;

namespace MenuTarro33.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IFlashMessage _flashMessage;
        private readonly IMapper _mapper;
        private readonly IImageVideoHelper _imageVideoHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriasController(ICategoriaRepository categoriaRepository, IFlashMessage flashMessage, IMapper mapper, IImageVideoHelper imageVideoHelper, IWebHostEnvironment webHostEnvironment)
        {
            _categoriaRepository = categoriaRepository;
            _flashMessage = flashMessage;
            _mapper = mapper;
            _imageVideoHelper = imageVideoHelper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
           var ListResponse = await _categoriaRepository.GetAllTblCategoriasAsync();
            return View(ListResponse.ListResults);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaDto = await _categoriaRepository.GetOnlyTblCategoriaAsync(id.Value);

            if (!categoriaDto.IsSuccess)
            {
                return NotFound();
            }

            return View(categoriaDto.Result);
        }
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new CategoriaDto());
            }
            else
            {
                var result = await _categoriaRepository.GetOnlyTblCategoriaAsync(id);
               
                if (!result.IsSuccess)
                {
                    return NotFound();
                }
                CategoriaDto category = result.Result;
                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, CategoriaDto category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _categoria = _mapper.Map<TbCategoria>(category);
                    GenericResponse<TbCategoria> genericResponse = new();
                    Guid imageId = Guid.Empty;
                    string path = string.Empty;
                    string Videopath = string.Empty;
                    if (id == 0) //Insert
                    {

                        if (category.ImageFile != null)
                        {
                            // imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                            path = await _imageVideoHelper.UploadImageAsync(category.ImageFile, "ImageCategoria");
                        }
                        if (category.VideoFile != null)
                        {
                            // imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                            Videopath = await _imageVideoHelper.UploadVideoAsync(category.VideoFile, "VideoCategoria");
                        }
                        _categoria.ImagePath = path;
                        _categoria.VideoPath = Videopath;
                        _categoria.Activo = 1;
                        _categoria.FechaRegistro = DateTime.Now.ToUniversalTime();


                        genericResponse = await _categoriaRepository.AddAsync(_categoria);
                        if (!genericResponse.IsSuccess) {
                            _flashMessage.Danger($"Error(In) en el Registro.  {genericResponse.Message}");
                        }

                        _flashMessage.Info("Registro creado.");
                    }
                    else //Update
                    {
                        path = _categoria.ImagePath;
                        Videopath = _categoria.VideoPath;
                        if (category.ImageFile != null)
                        {
                            // imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                            path = await _imageVideoHelper.UploadImageAsync(category.ImageFile, "ImageCategoria");
                        }
                        if (category.VideoFile != null)
                        {
                            // imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                            Videopath = await _imageVideoHelper.UploadVideoAsync(category.VideoFile, "VideoCategoria");
                        }
                        _categoria.ImagePath = path;
                        _categoria.VideoPath = Videopath;    
                        // await _categoryRepository.UpdateDataAsync(category);
                        genericResponse = await _categoriaRepository.UpdateAsync(_categoria);
                        if (!genericResponse.IsSuccess)
                        {
                            _flashMessage.Danger($"Error(Up) en el Registro.  {genericResponse.Message}");
                        }
                        _flashMessage.Info("Registro actualizado.");
                    }
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("Ya existe una categoría con el mismo nombre.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                    return View(category);
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                    return View(category);
                }
                var ListResponse = await _categoriaRepository.GetAllTblCategoriasAsync();
                return Json(new { isValid = true, html = ModalHelper.RenderRazorViewToString(this, "_ViewAll", ListResponse.ListResults.ToList()) });

            }

            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddOrEdit", category) });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var categoria = await _categoriaRepository.GetByIdAsync(Id.Value);
            if (!categoria.IsSuccess)
            {
                return NotFound();
            }

            // Eliminar la imagen
            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;
            string uploadVideo = _webHostEnvironment.WebRootPath + WC.VideoRuta;

            // borrar la imagen anterior
            var anteriorFile = Path.Combine(upload, categoria.Result.ImagePath);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }
            // fin Borrar imagen anterior
            // borrar la video anterior
            var anteriorFileVideo = Path.Combine(upload, categoria.Result.VideoPath);
            if (System.IO.File.Exists(anteriorFileVideo))
            {
                System.IO.File.Delete(anteriorFileVideo);
            }
            // fin Borrar video anterior

            var Result = await _categoriaRepository.DeleteAsync(categoria.Result.CategoriaId);
            if (!Result.IsSuccess)
            {
                TempData[WC.Error] = $"Error al pocesar esta Transaccion  {Result.Message}";
                return RedirectToAction(nameof(Index));
            }
            TempData[WC.Exitosa] = "Producto eliminado Exitosamente!";
            return RedirectToAction(nameof(Index));

        }
    }
}
