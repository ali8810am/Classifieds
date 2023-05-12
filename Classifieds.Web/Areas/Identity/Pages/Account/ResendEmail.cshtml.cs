using System.Text.Encodings.Web;
using Classifieds.Web.Model;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Classifieds.Web.Areas.Identity.Pages.Account
{
    public class ResendEmailModel : PageModel
    {
        private readonly IEmailSender _emailSender;

        public ResendEmailModel(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [BindProperty]
        public ResendEmailVm EmailVm { get; set; }
        public void OnGet(ResendEmailVm emailVm)
        {
            EmailVm.Email=emailVm.Email;
            EmailVm.CallbackUrl=emailVm.CallbackUrl;
        }

        public async Task OnPost()
        {
            try
            {
                await _emailSender.SendEmailAsync(EmailVm.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(EmailVm.CallbackUrl)}'>clicking here</a>.");
            }
            catch (Exception e)
            {
                 RedirectToPage("/Account/ConfirmEmail", EmailVm);
            }
        }


    }
}
