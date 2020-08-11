using Microsoft.AspNetCore.Authentication;
using SimpleCloudStorageServer.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Helper
{
    public static class AuthenticationBuilderExtentions
    {
        public static void AddApiKeyAuth(this AuthenticationBuilder authBuilder, Action<ApiKeyAuthOptions> options)
        {
            authBuilder.AddScheme<ApiKeyAuthOptions, ApiKeyAuth>(ApiKeyAuthOptions.DefaultScheme, options);
        }

    }
}
