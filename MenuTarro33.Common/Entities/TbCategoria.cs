using System.ComponentModel.DataAnnotations;

namespace MenuTarro33.Common.Entities
{
    public class TbCategoria
    {
        [Key]
        public  int CategoriaId { get; set; }
        
        public string NombreCategoria { get; set; }
        
        public string Descripcion { get; set; }
       
        public string ImagePath { get; set; }
       
        public string VideoPath { get; set; }
        public int Activo { get; set; } = 1;
        
        public DateTime FechaRegistro { get; set; }
       
        public DateTime DateLocal => FechaRegistro.ToLocalTime();
        public ICollection<TbPlatillo> TbPlatillos { get; set; }
    }
}
