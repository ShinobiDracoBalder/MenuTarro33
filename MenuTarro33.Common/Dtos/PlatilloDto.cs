using MenuTarro33.Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace MenuTarro33.Common.Dtos
{
    public class PlatilloDto
    {
        public int PlatilloId { get; set; }
        public string NombrePlatillo { get; set; }
        public string Descripcion { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Range(1, double.MaxValue, ErrorMessage = "Usted debe de seleccionar un Precio.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string ImagePath { get; set; }
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string VideoPath { get; set; }
        public int Activo { get; set; } = 1;
        public DateTime FechaRegistro { get; set; }
        [ForeignKey("TbCategoria")]
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public TbCategoria TbCategoria { get; set; }
        public ICollection<TbPlatilloImage> TbPlatilloImages { get; set; }
    }
}
