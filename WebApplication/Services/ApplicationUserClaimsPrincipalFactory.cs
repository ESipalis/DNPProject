using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApplication.Data;

namespace WebApplication.Services
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.IsManager ? "manager" : "user"));
            return identity;
        }
    }
}