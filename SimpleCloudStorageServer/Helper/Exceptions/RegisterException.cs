using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Helper.Exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException() : base("Registration error") { }
        public RegisterException(string message) : base(message) { }
    }
}
