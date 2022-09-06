using Microsoft.AspNetCore.Mvc.Rendering;

namespace MenuTarro33.Web.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync();
    }
}
