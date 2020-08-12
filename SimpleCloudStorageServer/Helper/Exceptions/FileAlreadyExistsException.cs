using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Helper.Exceptions
{
    public class FileAlreadyExistsException : Exception
    {
        public FileAlreadyExistsException() : base() { }
        public FileAlreadyExistsException(string message) : base(message) { }
    }
}
