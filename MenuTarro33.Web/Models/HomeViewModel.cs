using MenuTarro33.Common.Dtos;
using MenuTarro33.Common.Entities;
using MenuTarro33.Common.Utilities;

namespace MenuTarro33.Web.Models
{
    public class HomeViewModel
    {
        public PaginatedList<TbPlatillo> Platillos { get; set; }
        //public ICollection<ProductsHomeViewModel> Products { get; set; }

        //public ICollection<Product> Products { get; set; }

        //public ICollection<Category> Categories { get; set; }

        public float Quantity { get; set; }
    }
}
