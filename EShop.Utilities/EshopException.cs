using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Utilities
{
    public class EshopException : Exception
    {
        public EshopException()
        {

        }

        public EshopException(string message)
        {

        }

        public EshopException(string message, Exception inner):base(message,inner)
        {

        }
    }
}
