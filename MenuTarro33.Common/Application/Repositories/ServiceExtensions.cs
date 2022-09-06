using MenuTarro33.Common.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MenuTarro33.Common.Application.Repositories
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddTransient<ICategoriaRepository, CategoriaRepository>();
            service.AddTransient<IPlatilloRepository, PlatilloRepository>();
        }
    }
}
