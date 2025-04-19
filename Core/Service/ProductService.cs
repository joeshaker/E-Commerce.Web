using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
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

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Specifications = new ProductWithBrandAndTypeSpecification();
            var Products=await _unitOfWork.GetRepository<Product, int>().GetAllAsync( Specifications);
            return _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(Products);
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
            return _mapper.Map<Product,ProductDto>(Product);
             
        }
    }
}
