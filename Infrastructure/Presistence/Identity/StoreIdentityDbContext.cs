﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Identity
{
    public class StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable(name: "Addresses");
            builder.Entity<ApplicationUser>().ToTable(name: "Users");
            builder.Entity<IdentityRole>().ToTable(name: "Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable(name: "UserRoles");
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();
        }

    }

    
}
