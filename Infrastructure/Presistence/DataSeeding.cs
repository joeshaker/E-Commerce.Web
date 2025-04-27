using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                #region Delete
                //_dbContext.Products.RemoveRange(_dbContext.Products);
                //_dbContext.ProductTypes.RemoveRange(_dbContext.ProductTypes);
                //_dbContext.ProductBrands.RemoveRange(_dbContext.ProductBrands);
                //_dbContext.SaveChanges();
                #endregion
                await _dbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('ProductBrands', RESEED, 0)");
                await _dbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('ProductTypes', RESEED, 0)");
                await _dbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Products', RESEED, 0)");

                if (!_dbContext.Set<ProductBrand>().Any())
                {
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Presistence\Data\DataSeed\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                        await _dbContext.SaveChangesAsync();

                    }
                }
                if (!_dbContext.Set<ProductType>().Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Presistence\Data\DataSeed\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                        await _dbContext.SaveChangesAsync();

                    }
                }

                if (!_dbContext.Set<Product>().Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Presistence\Data\DataSeed\products.json");
                    var Products =  await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products != null && Products.Any())
                    {
                       await _dbContext.Products.AddRangeAsync(Products);
                    }
                }
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
