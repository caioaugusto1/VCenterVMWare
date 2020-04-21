using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Models;
using VSphere.Models.Identity;
using VSphere.Utils;

namespace VCenter.Controllers
{
    public class UserController : Controller
    {
        private readonly IOptions<AppSettings> _appSetttings;
        private readonly IUserApplication _userApplication;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _singManager;

        public UserController(IOptions<AppSettings> appSetttings, IUserApplication userApplication,
            UserManager<ApplicationIdentityUser> userManager, SignInManager<ApplicationIdentityUser> singManager)
        {
            _userApplication = userApplication;
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

            return View(_userApplication.GetAll());
        }

        public IActionResult MainLogin()
        {
            return View();
        }

        [HttpPost]
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
                var token = await JwtCreate(user.Email);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public void Logout()
        {
            _singManager.SignOutAsync();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            return Ok(await JwtCreate(user.Email));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View(_userApplication.GetById(id));
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

        private async Task<string> JwtCreate(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var jwtTokenHeadler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetttings.Value.Secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _appSetttings.Value.Issuer,
                Audience = _appSetttings.Value.ValidationIn,
                Expires = DateTime.UtcNow.AddHours(_appSetttings.Value.HoursToExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                     SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHeadler.CreateToken(tokenDescription);

            return jwtTokenHeadler.WriteToken(token);
        }
    }
}