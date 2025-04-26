using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Execptions;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper ) : IProductService
    {
        public async Task<IEnumerable<BrandsDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandDto= _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandsDto>>(Brands);
            return BrandDto;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Specifications = new ProductWithBrandAndTypeSpecification(queryParams);
            var AllProducts = await Repo.GetAllAsync( Specifications);
            var Data= _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(AllProducts);
            var ProductCount= Data.Count();
            var TotalCount= await Repo.CountAsync(new ProductCountSpecification(queryParams));
            return new PaginatedResult<ProductDto>(pageIndex:queryParams.PageIndex,pageSize:ProductCount,TotalCount,data:Data);
        }



        public async Task<IEnumerable<TypesDto>> GetAllTypesAsync()
        {
            var Types= await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto= _mapper.Map<IEnumerable<ProductType> , IEnumerable<TypesDto>>(Types);
            return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Specification=new ProductWithBrandAndTypeSpecification(id);       
            var Product= await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(Specification);
            if(Product is null)
            {
                throw new ProductNotFoundException(id);
            }
            return _mapper.Map<Product,ProductDto>(Product);
             
        }
    }
}
