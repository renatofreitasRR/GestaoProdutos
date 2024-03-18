using AutoMapper;
using GestaoProdutos.Application.Mappings;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Domain.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductMappingProfile));
        }
    }
}
