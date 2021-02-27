using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Notes.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Notes.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                var sendgridAuth = Environment.GetEnvironmentVariable("SG_KEY"); // Get the SendGrid API key

                // Use SendGrid if it's configured
                if (sendgridAuth != null)
                {

                    var sgClient = new SendGridClient(
                        sendgridAuth
                    );

                    // Create a new email to send with SendGrid
                    var email = MailHelper.CreateSingleEmail(
                        new EmailAddress("noreply@notes.2082.ca", "Notes application"),
                        new EmailAddress(Input.Email),
                        "Reset Password",
                        $"Please reset your password by copying and pasting this link into your browser: {callbackUrl}",
                        $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                    );

                    // Send the email, yielding until it does
                    await sgClient.SendEmailAsync(email);

                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                else
                {
                    // Assume the user is who they say they are, and allow them to reset the password
                    return RedirectToPage(callbackUrl);
                }
            }

            return Page();
        }
    }
}
