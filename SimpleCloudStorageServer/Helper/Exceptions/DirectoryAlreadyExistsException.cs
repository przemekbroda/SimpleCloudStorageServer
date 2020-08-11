using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Helper.Exceptions
{
    public class DirectoryAlreadyExistsException : Exception
    {
        public DirectoryAlreadyExistsException() : base() { }
        public DirectoryAlreadyExistsException(string message) : base(message) { }
    }
}
