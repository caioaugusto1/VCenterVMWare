﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using VCenter.Models;
using VCenter.Utils;
using VCenterVMWare.Application.Inteface;

namespace VCenter.Controllers
{
    public class UserController : Controller
    {
        private readonly IOptions<AppSettings> _appSetttings;
        private readonly IUserApplication _userApplication;

        public UserController(IOptions<AppSettings> appSetttings, IUserApplication userApplication)
        {
            _userApplication = userApplication;
            _appSetttings = appSetttings;
        }

        // GET: Login
        public ActionResult Index()
        {
            List<UserEntity> users = new List<UserEntity>();

            users = _userApplication.GetAll();


            //var client = new RestClient("https://10.100.11.37/rest/vcenter/vm/");
            //client.Authenticator = new HttpBasicAuthenticator("administrator@vsphere.local", "W@ster123");

            //var request = new RestRequest("statuses/home_timeline.json", DataFormat.Json);
            //IRestResponse<VM> vmsssss = client.Execute<VM>(request);

            return View(users);
        }

        public IActionResult MainLogin()
        {
            return View();
        }

        public IActionResult LogIn(string email, string password)
        {
            string cryptoPassword = Crypto.CryptoMd5(password);

            //var validarAcesso = _loginApplication.AutenticarAcesso(email, cryptoPassword);

            //if (validarAcesso == null)
            //{
            //    ModelState.AddModelError("login.Invalido", "Usuário ou senha Inválido, tente novamente!");
            //}
            //else
            //{
            //    ViewBag.Error = false;
            //    SessionManager.UsuarioLogado = validarAcesso;
            //    System.Web.Security.FormsAuthentication.SetAuthCookie(validarAcesso.Email, true);

            //    return RedirectToAction("Index", "Home");
            //}

            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public IActionResult Edit(string id)
        {
            var login = _userApplication.GetById(id);

            return View(login);
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}