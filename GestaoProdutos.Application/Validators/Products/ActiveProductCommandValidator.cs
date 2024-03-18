using FluentValidation;
using GestaoProdutos.Domain.Commands.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Application.Validators.Products
{
    public class ActiveProductCommandValidator : AbstractValidator<ActiveProductCommand>
    {
        public ActiveProductCommandValidator()
        {
           RuleFor(x => x.Id)
          .NotEmpty()
          .WithMessage("O campo Id está inválido")
          .NotNull()
          .WithMessage("O campo Id está inválido");
        }
    }
}
