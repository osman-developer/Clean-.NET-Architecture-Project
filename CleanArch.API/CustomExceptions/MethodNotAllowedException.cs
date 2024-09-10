using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.CustomExceptions
{
    public class MethodNotAllowedException : Exception
    {
        public MethodNotAllowedException() : base() { }

        public MethodNotAllowedException(string message) : base(message) { }
    }
}
