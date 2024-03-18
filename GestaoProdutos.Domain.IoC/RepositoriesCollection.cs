using Microsoft.Extensions.DependencyInjection;
using GestaoProdutos.Domain.Repositories;
using GestaoProdutos.Domain.Infra.Repositories;

namespace GestaoProdutos.Domain.IoC
{
    public static class RepositoriesCollection
    {
        public static IServiceCollection AddRepositoriesCollection(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
