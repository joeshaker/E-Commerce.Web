using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Identity;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _dbContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _storeIdentityDb) : IDataSeeding
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

        public async Task IdentityDataSeedAsync()
        {
            try 
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohammed@gmail.com",
                        DisplayName = "Mohammed Youssef",
                        UserName = "MohammedYoussef",
                        PhoneNumber = "01009653305",
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Rodina@gmail.com",
                        DisplayName = "Rodina Mohammed",
                        UserName = "RodinaMohammed",
                        PhoneNumber = "01009667305",
                    };
                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");

                }
                await _storeIdentityDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
