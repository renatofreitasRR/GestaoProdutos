using AutoMapper;
using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProdutos.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() 
        {
            CreateMap<Product, ProductViewModel>()
                .ReverseMap();
        }
    }
}
