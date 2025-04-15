using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var Products= await _serviceManager.ProductService.GetAllProductsAsync();
            return Ok(Products);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var Product= await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        [HttpGet("types")]

        public async Task<ActionResult<IEnumerable<TypesDto>>> GetTypes()
        {
            var Types= await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IEnumerable<BrandsDto>>> GetBrands() 
        {
            var Brands= await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }


    }
}
