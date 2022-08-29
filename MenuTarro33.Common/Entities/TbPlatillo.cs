using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace MenuTarro33.Common.Entities
{
    public class TbPlatillo
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
        [ForeignKey("ActivationsForm")]
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public TbCategoria TbCategoria { get; set; }
    }
}
