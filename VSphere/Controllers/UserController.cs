using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSphere.Models;
using VSphere.Models.Identity;
using VSphere.Utils;

namespace VCenter.Controllers
{
    //[Authorize(Roles = "Admin, Manager")]
    public class UserController : Controller
    {
        private readonly IOptions<AppSettings> _appSetttings;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _singManager;

        public UserController(IOptions<AppSettings> appSetttings,
            UserManager<ApplicationIdentityUser> userManager, SignInManager<ApplicationIdentityUser> singManager)
        {
            _appSetttings = appSetttings;
            _userManager = userManager;
            _singManager = singManager;
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
                    Insert = x.Insert
                });
            });

            return View(users);
        }

        [AllowAnonymous]
        public IActionResult MainLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return BadRequest("Usuário não encontrado");

            var result = await _singManager.PasswordSignInAsync(user.UserName, password, false, true);

            if (result.Succeeded)
            {
                //var token = await JwtCreate(user.Email);
                return RedirectToAction("Index", "Home");
            }

            return View();
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
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var userIdentity = new ApplicationIdentityUser
            {
                UserName = user.FullName,
                Email = user.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(userIdentity, user.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _singManager.SignInAsync(userIdentity, false);

            return Ok();

            //return Ok(await JwtCreate(user.Email));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View(_userManager.FindByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, UserViewModel user)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            if (user.Id != id)
                return null;

            return View();
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