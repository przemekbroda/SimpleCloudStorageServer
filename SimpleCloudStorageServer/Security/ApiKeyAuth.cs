using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleCloudStorageServer.Repository;
using SimpleCloudStorageServer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Security
{
    public class ApiKeyAuth : AuthenticationHandler<ApiKeyAuthOptions>
    {

        private const string ProblemDetailsContentType = "application/problem+json";

        private readonly IUserCachedService _userCachedService;
        private readonly IPasswordService _passwordService;

        public ApiKeyAuth(IOptionsMonitor<ApiKeyAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserCachedService userCachedService, IPasswordService passwordService) 
            : base(options, logger, encoder, clock)
        {
            _userCachedService = userCachedService;
            _passwordService = passwordService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.TryGetValue("AppId", out var appId)) return AuthenticateResult.NoResult();
            if (!Context.Request.Headers.TryGetValue("ApiKey", out var apiKey)) return AuthenticateResult.NoResult();

            var user = await _userCachedService.GetUser(appId);

            if (user == null) return AuthenticateResult.Fail("Invalid API Key or APP ID");

            if (!_passwordService.VerifyPassword(apiKey.FirstOrDefault(), user.ApiKeyHash, user.ApiKeySalt)) return AuthenticateResult.Fail("Invalid API Key or APP ID");

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "user"),
            };

            var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
            var identities = new List<ClaimsIdentity> { identity };
            var principal = new ClaimsPrincipal(identities);
            var ticket = new AuthenticationTicket(principal, Options.Scheme);

            return AuthenticateResult.Success(ticket);
        }


        

        
    }
}
