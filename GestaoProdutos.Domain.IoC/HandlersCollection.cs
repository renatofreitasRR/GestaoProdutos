using Microsoft.Extensions.DependencyInjection;
using GestaoProdutos.Domain.Handlers;

namespace GestaoProdutos.Domain.IoC
{
    public static class HandlersCollection
    {
        public static IServiceCollection AddHandlersCollection(this IServiceCollection services)
        {
            services.AddTransient<ProductHandler>();
            
            return services;
        }
    }
}
