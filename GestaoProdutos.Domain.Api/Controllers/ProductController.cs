using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GestaoProdutos.Domain.Commands;
using GestaoProdutos.Domain.Handlers;
using GestaoProdutos.Domain.Api.Controllers.Contracts;
using System.Net;
using GestaoProdutos.Domain.Repositories;
using GestaoProdutos.Domain.Commands.Products;
using GestaoProdutos.Domain.Models;
using GestaoProdutos.Domain.Entities;
using AutoMapper;
using GestaoProdutos.Domain.Models.Products;

namespace GestaoProdutos.Domain.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivePagedAsync([FromQuery] PaginationParams paginationParams)
        {
            var products = _productRepository
                .GetProductsActivePaged(paginationParams);

            var productsMapped = _mapper.Map<List<ProductViewModel>>(products.Items);

            var productsMappedPaged = new PagedResult<ProductViewModel>(productsMapped, products.TotalCount, products.CurrentPage, products.PageSize);

            return new CustomActionResult(HttpStatusCode.OK, productsMappedPaged);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInactivePagedAsync([FromQuery] PaginationParams paginationParams)
        {
            var products = _productRepository
                .GetProductsInactivePaged(paginationParams);

            var productsMapped = _mapper.Map<List<ProductViewModel>>(products.Items);

            var productsMappedPaged = new PagedResult<ProductViewModel>(productsMapped, products.TotalCount, products.CurrentPage, products.PageSize);

            return new CustomActionResult(HttpStatusCode.OK, productsMappedPaged);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productRepository
                .GetWithParamsAsync(x => x.Id == id);

            if (product == null)
                return new CustomActionResult(HttpStatusCode.NotFound, $"O Produto com id {id} não foi encontrado", isData: false);

            return new CustomActionResult(HttpStatusCode.OK, product);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromServices] ProductHandler handler, [FromBody] CreateProductCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromServices] ProductHandler handler, [FromBody] UpdateProductCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }

        [HttpPatch]
        public async Task<IActionResult> ActiveProductAsync([FromServices] ProductHandler handler, [FromBody] ActiveProductCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }

        [HttpPatch]
        public async Task<IActionResult> InactiveProductAsync([FromServices] ProductHandler handler, [FromBody] InactiveProductCommand command)
        {
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            return new CustomActionResult(commandResult.StatusCode, commandResult.Data, commandResult.Errors);
        }
    }
}
