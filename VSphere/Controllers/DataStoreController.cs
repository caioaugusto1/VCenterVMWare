using Microsoft.AspNetCore.Mvc;
using VSphere.Application.Interface;

namespace VSphere.Controllers
{
    public class DataStoreController : Controller
    {
        private readonly IDataStoreApplication _dataStoreApplication;

        public DataStoreController(IDataStoreApplication dataStoreApplication)
        {
            _dataStoreApplication = dataStoreApplication;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}