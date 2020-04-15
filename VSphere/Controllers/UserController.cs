﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using VCenter.Models;
using VCenter.Services;
using VCenter.Services.Inteface;
using VCenter.Utils;
using VCenterVMWare.Application.Inteface;
using VSphere.Models;

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
    }
}