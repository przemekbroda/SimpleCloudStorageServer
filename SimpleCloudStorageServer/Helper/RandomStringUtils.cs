using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Helper
{
    public static class RandomStringUtils
    {
        public static string GenerateRandomString()
        {
            var guid = Guid.NewGuid();
            return guid.ToString().Replace("-", string.Empty);
        }

        public static string GenerateApiKey() 
        {
            string apiKey;

            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] secretKeyByteArray = new byte[32]; //256 bit
                cryptoProvider.GetBytes(secretKeyByteArray);
                apiKey = Convert.ToBase64String(secretKeyByteArray);
            }

            return apiKey;
        }
    }
}
