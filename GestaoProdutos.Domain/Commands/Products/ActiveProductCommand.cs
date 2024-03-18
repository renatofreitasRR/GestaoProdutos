using GestaoProdutos.Domain.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Commands.Products
{
    public class ActiveProductCommand : ICommand
    {
        public int Id { get; set; }
    }
}
