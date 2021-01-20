using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.CataLog.ProductImages
{
    public class ProductImageCreateRequest 
    {
        public string Caption { get; set; }
        public bool IsDefualt { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
