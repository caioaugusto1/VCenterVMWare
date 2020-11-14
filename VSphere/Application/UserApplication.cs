using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Text;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Models.Identity;
using VSphere.Models.ViewModels.User;
using VSphere.Services;
using VSphere.Utils;

namespace VSphere.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _singManager;
        private readonly EmailHelper _emailHelper;
        private readonly RequestHandler _requestHandler;

        public UserApplication(UserManager<ApplicationIdentityUser> userManager, SignInManager<ApplicationIdentityUser> singManager, EmailHelper emailHelper, RequestHandler requestHandler)
        {
            _userManager = userManager;
            _singManager = singManager;
            _emailHelper = emailHelper;
            _requestHandler = requestHandler;
        }
        public async Task<ApplicationIdentityUser> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<bool> ResetPassword(UserResetPasswordViewModel model)
        {
            var entity = await _userManager.FindByEmailAsync(model.Email);

            if (entity == null || entity.Email != model.Email)
                return false;

            var code = TokenDecode(model.Token);
            var userChanged = await _userManager.ResetPasswordAsync(entity, code, model.Password);

            return userChanged.Succeeded;
        }

        public async Task<bool> ForgotPasswordConfirmation(string email)
        {
            var user = await _singManager.UserManager.FindByEmailAsync(email);

            if (user == null)
                return true;

            if (await GenerateCodeResetPasswordAndSendByEmail(user, "User", "ResetPassword", "Forgot Password", string.Empty))
                return true;
            else
                return false;
        }

        private async Task<bool> GenerateCodeResetPasswordAndSendByEmail(ApplicationIdentityUser user, string urlController, string urlAction, string emailSubject, string emailBody)
        {
            try
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = TokenEncode(code);

                var url = urlController + "/" + urlAction + "?userId=" + user.Id + "&code=" + code;
                SendEmail.Send(_requestHandler, _emailHelper, user.Email, emailSubject, emailBody);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string TokenEncode(string code)
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        }

        private string TokenDecode(string code)
        {
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }

    }
}
