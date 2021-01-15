using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.CataLog.Product
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public int Stock { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public string SeoTitle { get; set; }
        public IFormFile ThumNialImage { get; set; }
    }
}
