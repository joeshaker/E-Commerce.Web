using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                #region Delete
                //_dbContext.Products.RemoveRange(_dbContext.Products);
                //_dbContext.ProductTypes.RemoveRange(_dbContext.ProductTypes);
                //_dbContext.ProductBrands.RemoveRange(_dbContext.ProductBrands);
                //_dbContext.SaveChanges();
                #endregion
                _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('ProductBrands', RESEED, 0)");
                _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('ProductTypes', RESEED, 0)");
                _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Products', RESEED, 0)");

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Infrastructure\Presistence\Data\DataSeed\brands.json");
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                        _dbContext.SaveChanges();

                    }
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Presistence\Data\DataSeed\types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                        _dbContext.SaveChanges();

                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.ReadAllText(@"..\Infrastructure\Presistence\Data\DataSeed\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    if (Products != null && Products.Any())
                    {
                        _dbContext.Products.AddRange(Products);
                    }
                }
                _dbContext.SaveChanges();

            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
