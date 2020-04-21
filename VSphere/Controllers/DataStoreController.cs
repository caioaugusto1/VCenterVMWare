using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VSphere.Controllers
{
    public class DataStoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}