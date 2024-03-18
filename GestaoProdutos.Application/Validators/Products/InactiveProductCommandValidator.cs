using FluentValidation;
using GestaoProdutos.Domain.Commands.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Application.Validators.Products
{
    public class InactiveProductCommandValidator : AbstractValidator<InactiveProductCommand>
    {
        public InactiveProductCommandValidator()
        {
           RuleFor(x => x.Id)
          .NotEmpty()
          .WithMessage("O campo Id está inválido")
          .NotNull()
          .WithMessage("O campo Id está inválido");
        }
    }
}
