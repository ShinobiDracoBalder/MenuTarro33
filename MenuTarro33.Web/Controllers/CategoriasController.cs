using AutoMapper;
using MenuTarro33.Common.Application.Interfaces;
using MenuTarro33.Common.Application.Repositories;
using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Responses;
using MenuTarro33.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vereyon.Web;

namespace MenuTarro33.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IFlashMessage _flashMessage;
        private readonly IMapper _mapper;
        private readonly IImageVideoHelper _imageVideoHelper;

        public CategoriasController(ICategoriaRepository categoriaRepository, IFlashMessage flashMessage, IMapper mapper, IImageVideoHelper imageVideoHelper)
        {
            _categoriaRepository = categoriaRepository;
            _flashMessage = flashMessage;
            _mapper = mapper;
            _imageVideoHelper = imageVideoHelper;
        }
        public async Task<IActionResult> Index()
        {
           var ListResponse = await _categoriaRepository.GetAllTblCategoriasAsync();
            return View(ListResponse.ListResults);
        }
        //[NoDirectAccess]
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
                        Videopath = _categoria.ImagePath;
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
        
        //[HttpGet]
        //public async Task<IActionResult> Delete(int? Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var producto = await _productoRepo.ObtenerPrimeroAsync(p => p.ProductoId == Id, incluirPropiedades: "Categoria,TipoAplicacion");
        //    if (!producto.IsSuccess)
        //    {
        //        return NotFound();
        //    }

        //    // Eliminar la imagen
        //    string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;

        //    // borrar la imagen anterior
        //    var anteriorFile = Path.Combine(upload, producto.Result.ImagenUrl);
        //    if (System.IO.File.Exists(anteriorFile))
        //    {
        //        System.IO.File.Delete(anteriorFile);
        //    }
        //    // fin Borrar imagen anterior

        //    var Result = await _productoRepo.RemoverAsync(producto.Result);
        //    if (!Result.IsSuccess)
        //    {
        //        TempData[WC.Error] = $"Error al pocesar esta Transaccion  {Result.Message}";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    TempData[WC.Exitosa] = "Producto eliminado Exitosamente!";
        //    return RedirectToAction(nameof(Index));

        //}
    }
}
