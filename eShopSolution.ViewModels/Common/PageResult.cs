using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class PageResult<T> : PagedResultPage
    {
        public List<T> items { get; set; }
    }
}
