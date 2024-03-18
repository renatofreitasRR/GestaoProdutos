using GestaoProdutos.Domain.Commands;
using GestaoProdutos.Domain.Commands.Products;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Handlers;
using GestaoProdutos.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Tests
{
    [Collection(nameof(ProductCollection))]
    public class ProductTests
    {
        private readonly ProductTestsFixture _fixture;

        public ProductTests(ProductTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "New Product Valid")]
        [Trait("Categoria", "Product Fixture Testes")]
        public void Product_NewProduct_ShouldBeValid()
        {
            //Arrange
            var product = _fixture.GenerateValidProduct();

            //Act
            var validDate = product.IsValidProductDate();
            var validDoc = product.IsValidSupplierDocument();

            //Assert
            Assert.True(validDate);
            Assert.True(validDoc);
        }

        [Fact(DisplayName = "New Product Invalid")]
        [Trait("Categoria", "Product Fixture Testes")]
        public void Product_NewProduct_ShouldBeInvalid()
        {
            //Arrange
            var product = _fixture.GenerateInvalidProduct();

            //Act
            var validDate = product.IsValidProductDate();
            var validDoc = product.IsValidSupplierDocument();

            //Assert
            Assert.False(validDate);
            Assert.False(validDoc);
        }

        [Fact(DisplayName = "Valid PostAsync Method")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_ValidCommand_ReturnsCreated()
        {
            // Arrange
            var command = _fixture.GenerateValidCreateProductCommand();

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                     .ReturnsAsync(false);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.Created, commandResult.StatusCode);
            Assert.Empty(commandResult.Errors);
        }

        [Fact(DisplayName = "Invalid PostAsync Method - Description Conflict")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_CreateProductWithExistingDescription_ReturnsConflict()
        {
            // Arrange
            var command = _fixture.GenerateValidCreateProductCommand();

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                          .ReturnsAsync(true);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, commandResult.StatusCode);
            Assert.NotEmpty(commandResult.Errors);
            Assert.Contains($"O Produto com descrição {command.Description} já existe!", commandResult.Errors);
        }

        [Fact(DisplayName = "Invalid PostAsync Method - Invalid Date")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_CreateInvalidProductDate_ReturnsBadRequest()
        {
            // Arrange
            var command = _fixture.GenerateInvalidCreateProductCommandDateInvalid();

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                          .ReturnsAsync(false);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, commandResult.StatusCode);
            Assert.NotEmpty(commandResult.Errors);
            Assert.Contains($"Os Campos de Data estão inválidos, verifique os valores e tente novamente!", commandResult.Errors);
        }

        [Fact(DisplayName = "Invalid PostAsync Method - Invalid Supplier Document")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_CreateInvalidSupplierDocument_ReturnsBadRequest()
        {
            // Arrange
            var command = _fixture.GenerateInvalidCreateProductCommandDocumentInvalid();

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                          .ReturnsAsync(false);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, commandResult.StatusCode);
            Assert.NotEmpty(commandResult.Errors);
            Assert.Contains($"O campo Documento do Fornecedor é inválido", commandResult.Errors);
        }

        [Fact(DisplayName = "Valid PutAsync Method")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_UpdateValidCommand_ReturnsOk()
        {
            // Arrange
            var command = _fixture.GenerateValidUpdateProductCommand();

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                     .ReturnsAsync(false);

            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                     .ReturnsAsync(_fixture.GenerateValidProduct());

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.OK, commandResult.StatusCode);
            Assert.Empty(commandResult.Errors);
        }

        [Fact(DisplayName = "Invalid PutAsync Method - Invalid Date")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_UpdateInvalidProductDate_ReturnsBadRequest()
        {
            // Arrange
            var command = _fixture.GenerateInvalidUpdateProductCommandDateInvalid();

            var mockRepository = new Mock<IProductRepository>();

            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                     .ReturnsAsync(_fixture.GenerateValidProduct());

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, commandResult.StatusCode);
            Assert.NotEmpty(commandResult.Errors);
            Assert.Contains($"Os Campos de Data estão inválidos, verifique os valores e tente novamente!", commandResult.Errors);
        }

        [Fact(DisplayName = "Invalid PutAsync Method - Product Not Found")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_UpdateInvalidProduct_ReturnsNotFound()
        {
            // Arrange
            var command = _fixture.GenerateValidUpdateProductCommand();

            var mockRepository = new Mock<IProductRepository>();

            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                 .ReturnsAsync((Product)null);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, commandResult.StatusCode);
            Assert.NotEmpty(commandResult.Errors);
            Assert.Contains($"Produto não encontrado!", commandResult.Errors);
        }

        [Fact(DisplayName = "Invalid PutAsync Method - Invalid Supplier Document")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_UpdateInvalidSupplierDocument_ReturnsBadRequest()
        {
            // Arrange
            var command = _fixture.GenerateInvalidUpdateProductCommandDocumentInvalid();

            var mockRepository = new Mock<IProductRepository>();

            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                     .ReturnsAsync(_fixture.GenerateValidProduct());

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, commandResult.StatusCode);
            Assert.NotEmpty(commandResult.Errors);
            Assert.Contains($"O campo Documento do Fornecedor é inválido", commandResult.Errors);
        }

        [Fact(DisplayName = "Valid ActiveProductAsync Method - Product Activated Successfully")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_ProductActivated_Successfully()
        {
            // Arrange
            var product = _fixture.GenerateValidProduct(false);
            var command = new ActiveProductCommand { Id = product.Id };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                          .ReturnsAsync(product);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.OK, commandResult.StatusCode);
        }

        [Fact(DisplayName = "Invalid ActiveProductAsync Method - Product Already Active")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_ProductAlreadyActive_ReturnsBadRequest()
        {
            // Arrange
            var product = _fixture.GenerateValidProduct(true);
            var command = new ActiveProductCommand { Id = product.Id };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                          .ReturnsAsync(product);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, commandResult.StatusCode);
            Assert.Contains("O Produto já está ativo!", commandResult.Errors);
        }

        [Fact(DisplayName = "Invalid ActiveProductAsync Method - Product Not Found")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_ProductToActiveNotFound_ReturnsNotFound()
        {
            // Arrange
            var command = new ActiveProductCommand { Id = 1 };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                          .ReturnsAsync((Product)null);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, commandResult.StatusCode);
            Assert.Contains("Produto não encontrado!", commandResult.Errors);
        }

        [Fact(DisplayName = "Valid InactiveProductAsync Method - Product Inactivated Successfully")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_ProductInactivated_Successfully()
        {
            // Arrange
            var product = _fixture.GenerateValidProduct(true);
            var command = new InactiveProductCommand { Id = product.Id };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                          .ReturnsAsync(product);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.OK, commandResult.StatusCode);
        }

        [Fact(DisplayName = "Invalid InactiveProductAsync Method - Product Already Inactive")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_ProductAlreadyInactive_ReturnsBadRequest()
        {
            // Arrange
            var product = _fixture.GenerateValidProduct(false);
            var command = new InactiveProductCommand { Id = product.Id };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                          .ReturnsAsync(product);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, commandResult.StatusCode);
            Assert.Contains("O Produto já está inativo!", commandResult.Errors);
        }

        [Fact(DisplayName = "Invalid InactiveProductAsync Method - Product Not Found")]
        [Trait("Categoria", "Product Fixture Testes")]
        public async Task Handle_ProductToInactivateNotFound_ReturnsNotFound()
        {
            // Arrange
            var command = new InactiveProductCommand { Id = 1 };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetWithParamsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                          .ReturnsAsync((Product)null);

            var handler = new ProductHandler(mockRepository.Object);

            // Act
            CommandResult commandResult = (CommandResult)await handler.Handle(command);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, commandResult.StatusCode);
            Assert.Contains("Produto não encontrado!", commandResult.Errors);
        }
    }
}
