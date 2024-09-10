using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.CustomExceptions
{
    public class UnauthException : Exception
    {
        public UnauthException() : base() { }

        public UnauthException(string message) : base(message) { }
    }
}
