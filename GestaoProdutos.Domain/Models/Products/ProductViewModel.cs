using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Models.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public string? SupplierCode { get; set; }
        public string? SupplierDescription { get; set; }
        public string? SupplierDocument { get; set; }
    }
}
