using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eShopSolution.Data.EF
{
    class EShopDbContextFactory : IDesignTimeDbContextFactory<EShopDbContext>
    {
        public EShopDbContext CreateDbContext(string[] args)
        {
            //đọc chuỗi kết nối từ file json
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("eShopSolution");

            var optionBuilder = new DbContextOptionsBuilder<EShopDbContext>();
            optionBuilder.UseMySql(connectionString);

            return new EShopDbContext(optionBuilder.Options);
        }
    }
}
