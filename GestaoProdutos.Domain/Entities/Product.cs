using GestaoProdutos.Domain.Helpers;
using System.Linq.Expressions;

namespace GestaoProdutos.Domain.Entities
{
    public class Product
    {


        public void Update(string description, DateTime? dueDate, DateTime? manufacturingDate, string? supplierCode, string? supplierDescription, string? supplierDocument)
        {
            Description = description;
            DueDate = dueDate;
            ManufacturingDate = manufacturingDate;
            SupplierCode = supplierCode;
            SupplierDescription = supplierDescription;

            if (supplierDocument != null)
                SupplierDocument = ClearDocument(supplierDocument);
        }

        public void InactiveProduct()
        {
            this.IsActive = false;
        }

        public void ActiveProduct()
        {
            this.IsActive = true;
        }

        public bool IsValidProductDate()
        {
            if(this.ManufacturingDate >= this.DueDate)
                return false;
            else if(this.ManufacturingDate is not null && this.DueDate is null)
                return false;
            else
                return true;
        }


        protected Product()
        {

        }

        public Product(string description, DateTime? dueDate, DateTime? manufacturingDate, string? supplierCode, string? supplierDescription, string? supplierDocument, bool isActive = true)
        {
            Description = description;
            IsActive = isActive;
            DueDate = dueDate;
            ManufacturingDate = manufacturingDate;
            SupplierCode = supplierCode;
            SupplierDescription = supplierDescription;

            if (supplierDocument != null)
                SupplierDocument = ClearDocument(supplierDocument);
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime? ManufacturingDate { get; private set; }
        public string? SupplierCode { get; private set; }
        public string? SupplierDescription { get; private set; }
        public string? SupplierDocument { get; private set; }

        public bool IsValidSupplierDocument()
        {
            if (this.SupplierDocument is not null)
                return ValidateCNPJHelper.IsCnpj(this.SupplierDocument);
            else
                return true;
        }

        public string ClearDocument(string cnpj)
        {
            return cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
        }
    }
}
