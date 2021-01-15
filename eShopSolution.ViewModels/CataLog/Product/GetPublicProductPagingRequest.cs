using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.CataLog.Product
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
