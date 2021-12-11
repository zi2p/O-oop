#nullable enable
using System;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reports.Server.Database;

namespace Reports.Server.Authentication
{
    public class EmployeeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string SchemeName = "Employee";
        private readonly ReportsDatabaseContext _context;

        public EmployeeAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ReportsDatabaseContext context) : base(options,
            logger,
            encoder,
            clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            Endpoint? endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            return !authHeader.Scheme.Equals(SchemeName, StringComparison.InvariantCultureIgnoreCase) ? AuthenticateResult.Fail("Invalid Authorization Header Scheme") : AuthenticateResult.Success(null);
        }
    }
}