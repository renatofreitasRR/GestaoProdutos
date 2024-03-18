using GestaoProdutos.Domain.Commands.Contracts;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Domain.Commands.Products
{
    public class CreateProductCommand : ICommand
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string? SupplierCode { get; set; }
        public string? SupplierDescription { get; set; }
        public string? SupplierDocument { get; set; }
    }
}
