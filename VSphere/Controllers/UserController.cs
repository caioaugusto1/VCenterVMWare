using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using VSphere.Application.Interface;
using VSphere.Models;
using VSphere.Services.Inteface;
using VSphere.Utils;

namespace VCenter.Controllers
{
    public class UserController : Controller
    {
        private readonly IOptions<AppSettings> _appSetttings;
        private readonly IUserApplication _userApplication;
        private readonly IService<Object> _service;

        public UserController(IOptions<AppSettings> appSetttings, IUserApplication userApplication, IService<Object> service)
        {
            _userApplication = userApplication;
            _appSetttings = appSetttings;
            _service = service;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View(_userApplication.GetAll());
        }

        public IActionResult MainLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(string email, string password)
        {
            return RedirectToAction("Index", "Home");

            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
                return null;

            var user = _userApplication.GetByUserAndPassword(email, password);

            if (user == null)
            {
                ModelState.AddModelError("login.Invalido", "Incorrect User or Password, Try again!");
            }
            else
            {
                ViewBag.Error = false;
                //SessionManager.UsuarioLogado = user;
                //System.Web.Security.FormsAuthentication.SetAuthCookie(user.Email, true);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            return null;
            //Session.Abandon();
            //Session.RemoveAll();
            //return View("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                _userApplication.Insert(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("key", "message");
            }

            return View();
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
    }
}