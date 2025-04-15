using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObject;

namespace Service.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().
                ForMember(dest => dest.BrandName, Options => Options.MapFrom(src => src.ProductBrand.Name)).
                ForMember(dest => dest.TypeName, Options => Options.MapFrom(src => src.ProductType.Name)).
                ForMember(dest=>dest.PictureUrl,Options=>Options.MapFrom<PictureResolver>());

            CreateMap<ProductBrand, BrandsDto>();
            CreateMap<ProductType,TypesDto>();

        }
    }
}
