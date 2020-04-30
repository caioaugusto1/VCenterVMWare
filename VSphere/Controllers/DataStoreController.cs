using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using VSphere.Application.Interface;

namespace VSphere.Controllers
{
    [Authorize(Roles = "Admin, Manager, Datastore")]
    public class DataStoreController : Controller
    {
        private readonly IDataStoreApplication _dataStoreApplication;
        private readonly IServerApplication _serverApplication;

        public DataStoreController(IDataStoreApplication dataStoreApplication, IServerApplication serverApplication)
        {
            _dataStoreApplication = dataStoreApplication;
            _serverApplication = serverApplication;
        }

        public IActionResult Index()
        {
            ViewBag.Servers = _serverApplication.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByAPI(string apiId)
        {
            if (string.IsNullOrWhiteSpace(apiId))
                return Json(HttpStatusCode.Conflict);

            var dataFromAPI = await _dataStoreApplication.GetAllByApi(apiId);

            return Json(dataFromAPI);
        }

        [HttpGet]
        public IActionResult AllByAPI()
        {
            ViewBag.Servers = _serverApplication.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByFilterHistory(string apiId, string datetimeFrom, string datetimeTo)
        {
            if (string.IsNullOrWhiteSpace(apiId))
                return Json(HttpStatusCode.Conflict);

            return Json(_dataStoreApplication.GetAll(apiId, datetimeFrom, datetimeTo));
        }
    }
}