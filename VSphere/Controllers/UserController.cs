using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Models;
using VSphere.Models.Identity;
using VSphere.Services.Inteface;

namespace VCenter.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _singManager;
        private readonly IService _service;

        public UserController(UserManager<ApplicationIdentityUser> userManager,
            SignInManager<ApplicationIdentityUser> singManager, IService service)
        {
            _userManager = userManager;
            _singManager = singManager;
            _service = service;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            var usersFromDatabase = await _userManager.Users.ToListAsync();

            var users = new List<UserViewModel>();
            usersFromDatabase.ForEach(x =>
            {
                users.Add(new UserViewModel()
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Insert = x.Insert,
                    Enable = x.LockoutEnabled
                });
            });

            return View(users);
        }

        [AllowAnonymous]
        public IActionResult MainLogin()
        {
            //_service.SendEmail();

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
                ModelState.AddModelError("UserNotFound", "Usuário não encontrado!");
                return View(userLoginViewModel);
            }

            if (user.LockoutEnabled)
            {
                //ModelState.AddModelError("UserBlocked", "Seu usário está bloqueado, por favor, falar com o Administrador!");
                //return View(userLoginViewModel);
            }

            var result = await _singManager.PasswordSignInAsync(user.UserName, userLoginViewModel.Password, false, true);

            if (result.Succeeded)
            {
                //var token = await JwtCreate(user.Email);
                return RedirectToAction("Index", "Home");
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
                ModelState.AddModelError("UserFound", "User has already created!");
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
                LockoutEnabled = user.Enable
            };

            var findByEmail = await _userManager.FindByEmailAsync(user.Email);

            if (findByEmail != null)
            {
                ModelState.AddModelError("UserFound", "User has already created!");
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
            userViewModel.Enable = user.LockoutEnabled;

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel user)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            if (user.Id != id)
                return null;

            var userIdentity = new ApplicationIdentityUser
            {
                UserName = user.Email,
                FullName = user.FullName,
                Email = user.Email,
                LockoutEnabled = user.Enable
            };

            if (!String.IsNullOrWhiteSpace(user.Password))
                userIdentity.PasswordHash = user.Password;

            await _singManager.UserManager.UpdateAsync(userIdentity);

            return RedirectToAction("Index", "User");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Json(Conflict());

            var user = await _singManager.UserManager.FindByIdAsync(id);

            if (user != null)
                await _singManager.UserManager.DeleteAsync(user);

            return Json(Ok());
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
            userViewModel.Enable = user.LockoutEnabled;

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