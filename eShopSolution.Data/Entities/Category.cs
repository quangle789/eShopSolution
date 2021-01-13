using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public int SortOrder { get; set; }
        public bool IsShowOnHone { get; set; }
        public int? ParentId { get; set; }
        public Status Status { get; set; }
        public List<ProductInCategory> productInCategories { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
