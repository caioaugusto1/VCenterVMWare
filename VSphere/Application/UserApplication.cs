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

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = TokenEncode(code);

            var host = _requestHandler._httpContextAccessor.HttpContext.Request.Host;
            var scheme = _requestHandler._httpContextAccessor.HttpContext.Request.Scheme;

            var url = string.Format("{0}://{1}/{2}/{3}?userId={4}&code={5}", scheme, host, "User", "ResetPassword", user.Id, code);

            var emailBody = string.Format("<p><a href='{0}'> Clique aqui</a> para recuperação sua conta</p>", url);

            SendEmail.Send(_requestHandler, _emailHelper, user.Email, "Recuperação de Acesso", emailBody);

            return true;
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
