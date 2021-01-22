﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T result)
        {
            IsSuccessed = true;
            ResultObj = result;
        }
        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}
