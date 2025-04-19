using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecification:BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecification(ProductQueryParams queryParams) :
            base(P=> (!queryParams.BrandId.HasValue || P.BrandId==queryParams.BrandId) && 
            (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            &&(string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
            switch (queryParams.productSorting) 
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    break;
            }
            ApplyPagination(PageIndex:queryParams.PageIndex, PageSize:queryParams.PageSize);

        }
        public ProductWithBrandAndTypeSpecification(int id ) : base(P=>P.Id== id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
