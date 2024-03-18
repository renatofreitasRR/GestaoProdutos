using GestaoProdutos.Domain.Commands.Contracts;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Domain.Commands.Products
{
    public class UpdateProductCommand : ICommand
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string? SupplierCode { get; set; }
        public string? SupplierDescription { get; set; }
        public string? SupplierDocument { get; set; }
    }
}
