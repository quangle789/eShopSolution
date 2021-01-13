using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //data seeding
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig()
                {
                    Key = "HomeTitle",
                    Value = "This is home page of eOnlineShop"
                },
                new AppConfig()
                {
                    Key = "HomeKeyword",
                    Value = "This is keyword of eOnlineShop"
                },
                new AppConfig()
                {
                    Key = "HomeDescription",
                    Value = "This is description of eOnlineShop"
                });

            modelBuilder.Entity<Language>().HasData(
                    new Language()
                    {
                        Id = "vi-VN",
                        Name = "Tiếng việt",
                        IsDefault = true,
                    },
                    new Language()
                    {
                        Id = "en-US",
                        Name = "English",
                        IsDefault = true
                    }
                );

            modelBuilder.Entity<Category>().HasData(
                    new Category()
                    {
                        ID = 1,
                        IsShowOnHone = true,
                        ParentId = null,
                        SortOrder = 1,
                        Status = Status.Active,
                    },
                    new Category()
                    {
                        ID = 2,
                        IsShowOnHone = true,
                        ParentId = null,
                        SortOrder = 2,
                        Status = Status.Active,
                    }
                );

            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Áo nam",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm thời trang nam",
                    SeoTitle = "Áo thời trang nam",
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt product for men",
                    SeoTitle = "The shirt product for men",
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Áo nữ",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nu",
                    SeoDescription = "Sản phẩm thời trang nữ",
                    SeoTitle = "Áo thời trang nữ",
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "Women-shirt",
                    SeoDescription = "The shirt product for Women",
                    SeoTitle = "The shirt product for Women",
                });

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 1000000,
                    Price = 2000000,
                    Stock = 0,
                    ViewCount = 0,
                });

            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Áo sơ mi nam trắng Việt Tiến",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-so-mi-nam-trang-viet-tien",
                    SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                    SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                    Details = "Áo sơ mi nam trắng Việt Tiến",
                    Description = "Áo sơ mi nam trắng Việt Tiến",
                });

            modelBuilder.Entity<ProductInCategory>().HasData(
                    new ProductInCategory()
                    {
                        ProductId = 1,
                        CategoryId = 1,
                    }
                );

            //any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = adminId,
                Name ="admin",
                NormalizedName = "admin",
                Descreption = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "quanglm72@wru.vn",
                NormalizedEmail = "quanglm72@wru.vn",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FisrtName = "quang",
                LastName = "Le",
                DOB = new DateTime(1999,07,08)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
