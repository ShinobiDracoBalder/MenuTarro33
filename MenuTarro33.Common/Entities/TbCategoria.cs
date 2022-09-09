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
        public string PictureFullPath => ImagePath == string.Empty
          ? $"http://menutarro33.comtecom.com.mx/Images/noimage.png"
          : string.Format("http://menutarro33.comtecom.com.mx/{0}", ImagePath.Substring(1));
        public string VideoFullPath => VideoPath == string.Empty
                  ? null/*$"http://menutarro33.comtecom.com.mx/Images/noimage.png"*/
                  : string.Format("http://menutarro33.comtecom.com.mx/{0}", VideoPath.Substring(1));

    }
}
