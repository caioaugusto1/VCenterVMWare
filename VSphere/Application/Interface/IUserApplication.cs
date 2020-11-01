using System.Threading.Tasks;
using VSphere.Models.Identity;
using VSphere.Models.ViewModels.User;

namespace VSphere.Application.Interface
{
    public interface IUserApplication
    {
        Task<bool> ForgotPasswordConfirmation(string email);

        Task<ApplicationIdentityUser> GetById(string id);

        Task<bool> ResetPassword(UserResetPasswordViewModel model);
    }
}
