using System.Security.Claims;
using Classifieds.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Classifieds.Web.Services.Identity
{
    public class CustomClaimsService : UserClaimsPrincipalFactory<User>
    {
        public CustomClaimsService(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected async override Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(UserClaims.FullName, $"{user.FirstName} {user.LastName}"));
            return identity;


        }
    }
}
