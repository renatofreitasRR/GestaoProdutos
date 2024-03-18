using Microsoft.EntityFrameworkCore;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Infra.Context;
using GestaoProdutos.Domain.Models;
using GestaoProdutos.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace GestaoProdutos.Domain.Infra.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public PagedResult<Product> GetProductsActivePaged(PaginationParams paginationParams)
        {
            var products = _context
              .Products
              .AsNoTracking()
              .OrderBy(x => x.Description)
              .Where(x => x.IsActive == true)
              .AsQueryable();

            if (string.IsNullOrEmpty(paginationParams.Search) is false)
            {
                var upperText = paginationParams.Search.ToUpper();

                products = products
                    .Where(x => x.Description.ToUpper().Contains(upperText))
                    .AsQueryable();
            }

            var pagedProducts = PagedList<Product>
                .ToPagedList(products, paginationParams.PageNumber, paginationParams.PageSize);

            return pagedProducts;
        }

        public PagedResult<Product> GetProductsInactivePaged(PaginationParams paginationParams)
        {
            var products = _context
              .Products
              .AsNoTracking()
              .OrderBy(x => x.Description)
              .Where(x => x.IsActive == false)
              .AsQueryable();

            if (string.IsNullOrEmpty(paginationParams.Search) is false)
            {
                var upperText = paginationParams.Search.ToUpper();

                products = products
                    .Where(x => x.Description.ToUpper().Contains(upperText))
                    .AsQueryable();
            }

            var pagedProducts = PagedList<Product>
                .ToPagedList(products, paginationParams.PageNumber, paginationParams.PageSize);

            return pagedProducts;
        }
    }
}
