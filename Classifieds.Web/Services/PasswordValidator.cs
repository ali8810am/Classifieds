using Classifieds.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Classifieds.Web.Services
{
    public class PasswordValidator:IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password.Contains(user.FirstName)|| password.Contains(user.LastName)|| password.Contains(user.UserName))
            {
                var error = new IdentityError
                {
                    Code = "Weak Password",
                    Description = "Password should not contains name or UserName"
                };
                errors.Add(error);
            }
            if (password.Contains(user.Email))
            {
                var error = new IdentityError
                {
                    Code = "Weak Password",
                    Description = "Password should not contains email"
                };
                errors.Add(error);
            }

            if (errors.Count>0)
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            else
            {
                return Task.FromResult(IdentityResult.Success);
            }
        }
    }
}
