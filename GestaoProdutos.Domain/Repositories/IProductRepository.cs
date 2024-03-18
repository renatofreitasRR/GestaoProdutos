using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Models;

namespace GestaoProdutos.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        PagedResult<Product> GetProductsActivePaged(PaginationParams paginationParams);
        PagedResult<Product> GetProductsInactivePaged(PaginationParams paginationParams);
    }
}
