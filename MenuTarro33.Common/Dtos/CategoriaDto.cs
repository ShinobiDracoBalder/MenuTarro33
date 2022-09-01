using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MenuTarro33.Common.Dtos
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Nombre Categoría")]
        public string NombreCategoria { get; set; }
        [MaxLength(250, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string ImagePath { get; set; }
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string VideoPath { get; set; }
        public int Activo { get; set; } = 1;
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime FechaRegistro { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateLocal => FechaRegistro.ToLocalTime();

        public IFormFile ImageFile { get; set; }

        public IFormFile VideoFile { get; set; }
        [Display(Name = "Image")]
        public string PictureFullPath { get; set; }
        [Display(Name = "Video")]
        public string VideoFullPath { get; set; }
    }
}
