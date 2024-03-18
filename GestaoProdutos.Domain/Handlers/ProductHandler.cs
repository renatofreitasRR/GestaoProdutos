using GestaoProdutos.Domain.Commands;
using GestaoProdutos.Domain.Commands.Contracts;
using GestaoProdutos.Domain.Commands.Products;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Handlers.Contracts;
using GestaoProdutos.Domain.Repositories;
using System.Net;

namespace GestaoProdutos.Domain.Handlers
{
    public class ProductHandler : 
        IHandler<CreateProductCommand>,
        IHandler<UpdateProductCommand>,
        IHandler<ActiveProductCommand>,
        IHandler<InactiveProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ICommandResult> Handle(CreateProductCommand command)
        {
            var productExists = await _productRepository
                .ExistsAsync(x => x.Description == command.Description);

            if (productExists)
                return new CommandResult(HttpStatusCode.Conflict, null, $"O Produto com descrição {command.Description} já existe!");

            var product = new Product(command.Description, command.DueDate, command.ManufacturingDate, command.SupplierCode, command.SupplierDescription, command.SupplierDocument);

            if (product.IsValidSupplierDocument() is false)
                return new CommandResult(HttpStatusCode.BadRequest, null, $"O campo Documento do Fornecedor é inválido");

            if(product.IsValidProductDate() is false)
                return new CommandResult(HttpStatusCode.BadRequest, null, $"Os Campos de Data estão inválidos, verifique os valores e tente novamente!");

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveAsync();

            return new CommandResult(HttpStatusCode.Created);
        }

        public async Task<ICommandResult> Handle(UpdateProductCommand command)
        {
            var product = await _productRepository.GetWithParamsAsync(x => x.Id == command.Id);

            if (product is null)
                return new CommandResult(HttpStatusCode.NotFound, null, $"Produto não encontrado!");

            product.Update(command.Description, command.DueDate, command.ManufacturingDate, command.SupplierCode, command.SupplierDescription, command.SupplierDocument);

            if (product.IsValidSupplierDocument() is false)
                return new CommandResult(HttpStatusCode.BadRequest, null, $"O campo Documento do Fornecedor é inválido");

            if (product.IsValidProductDate() is false)
                return new CommandResult(HttpStatusCode.BadRequest, null, $"Os Campos de Data estão inválidos, verifique os valores e tente novamente!");

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return new CommandResult();
        }

        public async Task<ICommandResult> Handle(ActiveProductCommand command)
        {
            var product = await _productRepository.GetWithParamsAsync(x => x.Id == command.Id);

            if (product is null)
                return new CommandResult(HttpStatusCode.NotFound, null, $"Produto não encontrado!");

            if (product.IsActive)
                return new CommandResult(HttpStatusCode.BadRequest, null, $"O Produto já está ativo!");

            product.ActiveProduct();

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return new CommandResult();
        }

        public async Task<ICommandResult> Handle(InactiveProductCommand command)
        {
            var product = await _productRepository.GetWithParamsAsync(x => x.Id == command.Id);

            if (product is null)
                return new CommandResult(HttpStatusCode.NotFound, null, $"Produto não encontrado!");

            if(product.IsActive is false)
                return new CommandResult(HttpStatusCode.BadRequest, null, $"O Produto já está inativo!");

            product.InactiveProduct();

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return new CommandResult();
        }

    }
}
