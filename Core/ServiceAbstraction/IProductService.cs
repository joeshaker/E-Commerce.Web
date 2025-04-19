using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObject;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        public Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);

        public Task<ProductDto> GetProductByIdAsync(int id);

        public Task<IEnumerable<TypesDto>> GetAllTypesAsync();

        public Task<IEnumerable<BrandsDto>> GetAllBrandsAsync();


    }
}
