using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Enums;
using VSphere.Models;
using VSphere.Models.Identity;
using VSphere.Models.ViewModels.User;
using VSphere.Services.Inteface;

namespace VCenter.Controllers
{
    //[Authorize(Roles = "Admin, Manager")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _singManager;
        private readonly IUserApplication _userApplication;
        private readonly IService _service;

        public UserController(UserManager<ApplicationIdentityUser> userManager,
            SignInManager<ApplicationIdentityUser> singManager, IService service, IUserApplication userApplication)
        {
            _userManager = userManager;
            _singManager = singManager;
            _service = service;
            _userApplication = userApplication;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            var usersFromDatabase = await _userManager.Users.ToListAsync();

            var users = new List<UserViewModel>();
            foreach (var user in usersFromDatabase)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Contains(RolesTypes.Admin.ToString()))
                {
                    users.Add(new UserViewModel()
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Insert = user.Insert,
                        LockoutEnabled = user.LockoutEnabled
                    });
                }
            }

            return View(users);
        }

        [AllowAnonymous]
        public async Task<IActionResult> MainLogin()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return View();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Test(UserResetPasswordViewModel model)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> MainLogin(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid)
                return View(userLoginViewModel);

            var user = await _userManager.FindByEmailAsync(userLoginViewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("UserNotFound", "Usuário ou senha inválidos!");
                return View(userLoginViewModel);
            }

            if (user.LockoutEnabled)
            {
                ModelState.AddModelError("UserBlocked", "Seu usário está bloqueado, por favor, falar com o Administrador!");
                return View(userLoginViewModel);
            }

            var result = await _singManager.PasswordSignInAsync(user.UserName, userLoginViewModel.Password, false, true);

            if (result.Succeeded)
            {
                //var token = await JwtCreate(user.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("UserNotFound", "Usuário ou senha inválidos!");
                return View(userLoginViewModel);
            }

            return View(userLoginViewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _singManager.SignOutAsync();
            return RedirectToAction("MainLogin", "User");
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Json(HttpStatusCode.BadRequest);

            var result = await _userApplication.ForgotPasswordConfirmation(email);

            if (result)
                return Json(new { statusCode = HttpStatusCode.NoContent });

            return Json(new { statusCode = HttpStatusCode.NotFound });
        }


        [HttpGet]
        public async Task<IActionResult> ForgotPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
                return View("Error");

            var user = await _userApplication.GetById(userId);
            if (user == null)
                return RedirectToAction("Index", "Home");

            var model = new UserResetPasswordViewModel();
            model.Id = userId;
            model.Token = code;

            return View("ResetPassword", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(UserResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userApplication.ResetPassword(model);

            return RedirectToAction("ResetPasswordConfirmation", "User");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var userIdentity = new ApplicationIdentityUser
            {
                UserName = user.Email,
                FullName = user.FullName,
                Email = user.Email,
                Insert = DateTime.Now,
                LockoutEnabled = true
            };

            var findByEmail = await _userManager.FindByEmailAsync(user.Email);

            if (findByEmail != null)
            {
                ModelState.AddModelError("UserFound", "Usuário já foi criado!");
                return View(user);
            }

            var result = await _userManager.CreateAsync(userIdentity, user.Password);

            if (!result.Succeeded)
                return View(user);

            await _singManager.SignInAsync(userIdentity, false);

            return RedirectToAction("Index", "Home");

            //return Ok(await JwtCreate(user.Email));
        }

        [HttpGet]
        public IActionResult InCreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InCreateUser(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var userIdentity = new ApplicationIdentityUser
            {
                UserName = user.Email,
                FullName = user.FullName,
                Email = user.Email,
                Insert = DateTime.Now,
                PasswordHash = user.Password,
                LockoutEnabled = user.LockoutEnabled
            };

            var findByEmail = await _userManager.FindByEmailAsync(user.Email);

            if (findByEmail != null)
            {
                ModelState.AddModelError("UserFound", "Esse usuário já foi criado!");
                return View(user);
            }

            var result = await _userManager.CreateAsync(userIdentity, user.Password);

            if (!result.Succeeded)
                return View(user);

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userViewModel = new UserViewModel();

            userViewModel.Id = user.Id.ToString();
            userViewModel.FullName = user.FullName;
            userViewModel.Email = user.Email;
            userViewModel.Insert = user.Insert;
            userViewModel.LockoutEnabled = user.LockoutEnabled;

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel user)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                ModelState.AddModelError(string.Empty, "Id do Usuário não encontrado");
                return View(user);
            }

            //var userIdentity = new ApplicationIdentityUser
            //{
            //    UserName = user.Email,
            //    FullName = user.FullName,
            //    Email = user.Email,
            //    LockoutEnabled = user.Enable
            //};

            var userById = await _userManager.FindByIdAsync(id);
            userById.FullName = user.FullName;
            userById.LockoutEnabled = user.LockoutEnabled;

            await _userManager.UpdateAsync(userById);

            return RedirectToAction("Index", "User");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Json(new { StatusCode = HttpStatusCode.Conflict });

            var user = await _singManager.UserManager.FindByIdAsync(id);

            if (user != null)
                await _singManager.UserManager.DeleteAsync(user);

            return Json(new { StatusCode = HttpStatusCode.OK });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            var user = await _userManager.FindByIdAsync(id);

            var userViewModel = new UserViewModel();

            userViewModel.Id = user.Id.ToString();
            userViewModel.FullName = user.FullName;
            userViewModel.Email = user.Email;
            userViewModel.Insert = user.Insert;
            userViewModel.LockoutEnabled = user.LockoutEnabled;

            return View(userViewModel);
        }

        #region If you would like to use JWT Authentication and you will change the .NET just to working with WebAPI

        //private async Task<string> JwtCreate(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);

        //    var identityClaims = new ClaimsIdentity();
        //    identityClaims.AddClaims(await _userManager.GetClaimsAsync(user));

        //    var jwtTokenHeadler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSetttings.Value.Secret);

        //    var tokenDescription = new SecurityTokenDescriptor
        //    {
        //        Subject = identityClaims,
        //        Issuer = _appSetttings.Value.Issuer,
        //        Audience = _appSetttings.Value.ValidationIn,
        //        Expires = DateTime.UtcNow.AddHours(_appSetttings.Value.HoursToExpire),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //             SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = jwtTokenHeadler.CreateToken(tokenDescription);

        //    return jwtTokenHeadler.WriteToken(token);
        //}

        #endregion
    }
}