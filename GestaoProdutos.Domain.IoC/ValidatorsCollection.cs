using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using GestaoProdutos.Application.Validators.Products;

namespace GestaoProdutos.Domain.IoC
{
    public static class ValidatorsCollection
    {
        public static IServiceCollection AddValidatorsCollection(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateProductCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<ActiveProductCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<InactiveProductCommandValidator>();

            return services;
        }
    }
}
