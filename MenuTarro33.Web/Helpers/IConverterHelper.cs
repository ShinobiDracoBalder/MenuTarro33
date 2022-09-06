using MenuTarro33.Common.Entities;
using MenuTarro33.Web.Models;

namespace MenuTarro33.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<TbPlatillo> ToPlatilloAsync(PlatilloViewModel model, bool isNew);
        Task<TbPlatillo> ToPlatilloUPAsync(PlatilloViewModel model, bool isNew);
    }
}
