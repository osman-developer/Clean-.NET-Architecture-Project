using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.CustomExceptions
{
    public static class StatusCodeMessage
    {
        public static String Code403 { get { return "Access forbidden"; } }
        public static String Code401 { get { return "Unauthorized, please check your email or password"; } }
        public static String Code404 { get { return "Request Not Found"; } }
        public static String Code500 { get { return "InternalServerError"; } }
    }
}
