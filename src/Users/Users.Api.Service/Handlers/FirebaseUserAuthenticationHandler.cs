using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PasswordManager.Users.Infrastructure.UserRepository;

namespace PasswordManager.Users.Api.Service.Handlers
{
    public class FirebaseUserAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string BEARER_PREFIX = "Bearer ";

        private readonly FirebaseApp _firebaseApp;
        private readonly ILogger<FirebaseUserAuthenticationHandler> _logger;
        protected readonly UserContext _context;

        public FirebaseUserAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            FirebaseApp firebaseApp,
            UserContext context)
            : base(options, logger, encoder, clock)
        {
            _firebaseApp = firebaseApp;
            this._logger = logger.CreateLogger<FirebaseUserAuthenticationHandler>();
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            this._logger.LogInformation("Calling HandleAuthenticateAsync for FirebaseUserAuthenticationHandler");
            if (!Context.Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("No authorization");
            }

            string? bearerToken = Context.Request.Headers["Authorization"];

            //Remove this when development is done
            if (bearerToken == "96463f61-5783-42e8-97f3-d17d77ae7c76")
            {
                FirebaseToken firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync("rnd8SmV34yW3ORciThY94vaQ2Bo2");

                return AuthenticateResult.Success(CreateAuthenticationTicket(firebaseToken));
            }

            if (bearerToken == null || !bearerToken.StartsWith(BEARER_PREFIX))
            {
                return AuthenticateResult.Fail("Invalid scheme.");
            }

            string token = bearerToken.Substring(BEARER_PREFIX.Length);

            try
            {
                FirebaseToken firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(token);

                return AuthenticateResult.Success(CreateAuthenticationTicket(firebaseToken));
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }
        }

        private AuthenticationTicket CreateAuthenticationTicket(FirebaseToken firebaseToken)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(ToClaims(firebaseToken.Claims), nameof(ClaimsIdentity))
            });

            return new AuthenticationTicket(claimsPrincipal, JwtBearerDefaults.AuthenticationScheme);
        }

        private IEnumerable<Claim> ToClaims(IReadOnlyDictionary<string, object> claims)
        {
            return new List<Claim>
            {
                new Claim(FirebaseUserClaimType.ID, claims.GetValueOrDefault("user_id", "").ToString()!),
                new Claim(FirebaseUserClaimType.EMAIL, claims.GetValueOrDefault("email", "").ToString()!),
                new Claim(FirebaseUserClaimType.EMAIL_VERIFIED, claims.GetValueOrDefault("email_verified", "").ToString()!),
                new Claim(FirebaseUserClaimType.USERNAME, claims.GetValueOrDefault("name", "").ToString()!),
            };
        }

        

        
    }
}