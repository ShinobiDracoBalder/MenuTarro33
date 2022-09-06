using MenuTarro33.Common.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace MenuTarro33.Web.Models
{
    public class PlatilloViewModel
    {
        public int PlatilloId { get; set; }
        
        [Display(Name = "Nombre Platillo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string NombrePlatillo { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(1550, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        //[Range(1, double.MaxValue, ErrorMessage = "Usted debe de seleccionar un Precio.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [MaxLength(1500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string ImagePath { get; set; }

        [MaxLength(1500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string VideoPath { get; set; }
        public int Activo { get; set; } = 1;
        public DateTime FechaRegistro { get; set; }
        
        [Display(Name = "Categoria")]
        [Range(1, double.MaxValue, ErrorMessage = "Usted debe de seleccionar una Categoria.")]
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public TbCategoria TbCategoria { get; set; }
        public string PictureFullPath { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IFormFile ImageFile { get; set; }
        public IFormFile VideoFile { get; set; }
    }
}
