using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MenuTarro33.Common.Entities
{
    public class TbPlatillo
    {
        [Key]
        public int PlatilloId { get; set; }
        public string NombrePlatillo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string ImagePath { get; set; }
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
