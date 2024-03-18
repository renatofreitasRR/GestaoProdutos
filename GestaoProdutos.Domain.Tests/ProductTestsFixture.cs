using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using GestaoProdutos.Domain.Commands.Products;
using GestaoProdutos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GestaoProdutos.Domain.Tests
{
    public class ProductTestsFixture : IDisposable
    {
        public Product GenerateValidProduct(bool active = true)
        {

            var product = new Faker<Product>("pt_BR")
            .CustomInstantiator(f => new Product(
                f.Commerce.ProductName(),
                f.Date.Future(10),
                f.Date.Past(10),
                f.Random.AlphaNumeric(10),
                f.Company.CompanyName(),
                f.Company.Cnpj(),
                isActive: active
                ));

            return product;
        }

        public CreateProductCommand GenerateValidCreateProductCommand()
        {

            var createCommand = new Faker<CreateProductCommand>("pt_BR")
            .CustomInstantiator(f => new CreateProductCommand
            {
                Description = f.Commerce.ProductName(),
                DueDate = f.Date.Future(10),
                ManufacturingDate = f.Date.Past(10),
                SupplierCode = f.Random.AlphaNumeric(10),
                SupplierDescription = f.Company.CompanyName(),
                SupplierDocument = f.Company.Cnpj()
            });

            return createCommand;
        }

        public UpdateProductCommand GenerateValidUpdateProductCommand()
        {

            var updateCommand = new Faker<UpdateProductCommand>("pt_BR")
            .CustomInstantiator(f => new UpdateProductCommand
            {
                Description = f.Commerce.ProductName(),
                DueDate = f.Date.Future(10),
                ManufacturingDate = f.Date.Past(10),
                SupplierCode = f.Random.AlphaNumeric(10),
                SupplierDescription = f.Company.CompanyName(),
                SupplierDocument = f.Company.Cnpj(),
                Id = 14
            });

            return updateCommand;
        }

        public CreateProductCommand GenerateInvalidCreateProductCommandDateInvalid()
        {

            var createCommand = new Faker<CreateProductCommand>("pt_BR")
            .CustomInstantiator(f => new CreateProductCommand
            {
                Description = f.Commerce.ProductName(),
                DueDate = f.Date.Past(10),
                ManufacturingDate = f.Date.Future(10),
                SupplierCode = f.Random.AlphaNumeric(10),
                SupplierDescription = f.Company.CompanyName(),
                SupplierDocument = f.Company.Cnpj()
            });

            return createCommand;
        }

        public UpdateProductCommand GenerateInvalidUpdateProductCommandDateInvalid()
        {

            var updateCommand = new Faker<UpdateProductCommand>("pt_BR")
            .CustomInstantiator(f => new UpdateProductCommand
            {
                Description = f.Commerce.ProductName(),
                DueDate = f.Date.Past(10),
                ManufacturingDate = f.Date.Future(10),
                SupplierCode = f.Random.AlphaNumeric(10),
                SupplierDescription = f.Company.CompanyName(),
                SupplierDocument = f.Company.Cnpj(),
                Id = 14
            });

            return updateCommand;
        }

        public CreateProductCommand GenerateInvalidCreateProductCommandDocumentInvalid()
        {

            var createCommand = new Faker<CreateProductCommand>("pt_BR")
            .CustomInstantiator(f => new CreateProductCommand
            {
                Description = f.Commerce.ProductName(),
                DueDate = f.Date.Future(10),
                ManufacturingDate = f.Date.Past(10),
                SupplierCode = f.Random.AlphaNumeric(10),
                SupplierDescription = f.Company.CompanyName(),
                SupplierDocument = f.Random.AlphaNumeric(10)
            });

            return createCommand;
        }

        public UpdateProductCommand GenerateInvalidUpdateProductCommandDocumentInvalid()
        {

            var updateCommand = new Faker<UpdateProductCommand>("pt_BR")
            .CustomInstantiator(f => new UpdateProductCommand
            {
                Description = f.Commerce.ProductName(),
                DueDate = f.Date.Future(10),
                ManufacturingDate = f.Date.Past(10),
                SupplierCode = f.Random.AlphaNumeric(10),
                SupplierDescription = f.Company.CompanyName(),
                SupplierDocument = f.Random.AlphaNumeric(10),
                Id = 14
            });

            return updateCommand;
        }

        public Product GenerateInvalidProduct()
        {
            var product = new Faker<Product>("pt_BR")
            .CustomInstantiator(f => new Product(
                f.Vehicle.Random.String(),
                f.Date.Past(10),
                f.Date.Future(10),
                f.Random.AlphaNumeric(10),
                f.Company.CompanyName(),
                f.Random.AlphaNumeric(10)
                ));

            return product;
        }

        public void Dispose()
        {
        }
    }
}
