using Microsoft.OpenApi.Models;

namespace GestaoProdutos.Domain.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App.API", Version = "v1" });
            });
        }
    }
}
