using FluentValidation;
using GestaoProdutos.Domain.Commands.Products;

namespace GestaoProdutos.Application.Validators.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Description)
           .NotEmpty()
           .MinimumLength(5)
           .WithMessage("O campo Descrição está inválido, digite entre 5 e 255 caracteres")
           .MaximumLength(255)
           .WithMessage("O campo Descrição está inválido, digite entre 5 e 255 caracteres");

            RuleFor(x => x.SupplierCode)
           .MinimumLength(5)
           .WithMessage("O campo Descrição está inválido, digite entre 5 e 255 caracteres")
           .MaximumLength(255)
           .WithMessage("O campo Código do Fornecedor está inválido, digite entre 5 e 255 caracteres");

            RuleFor(x => x.SupplierDocument)
           .MinimumLength(14)
           .WithMessage("O campo Documento do Fornecedor está inválido, digite entre 14 e 18 caracteres")
           .MaximumLength(18)
           .WithMessage("O campo Documento do Fornecedor está inválido, digite entre 14 e 18 caracteres");

            RuleFor(x => x.SupplierDescription)
           .MinimumLength(5)
           .WithMessage("O campo Descrição do Fornecedor está inválido, digite entre 5 e 255 caracteres")
           .MaximumLength(255)
           .WithMessage("O campo Descrição do Fornecedor está inválido, digite entre 5 e 255 caracteres");

            RuleFor(x => x.ManufacturingDate)
           .LessThan(x => x.DueDate)
           .WithMessage("O campo Data de Fabricação não pode ser maior ou igual que a data de validade");
        }
    }
}
