using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecification:BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecification(int ?BrandId , int ?TypeId) :
            base(P=> (!BrandId.HasValue || P.BrandId==BrandId) && (!TypeId.HasValue || P.TypeId==TypeId))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
        public ProductWithBrandAndTypeSpecification(int id ) : base(P=>P.Id== id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
