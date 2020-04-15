using Microsoft.AspNetCore.Mvc;
using VSphere.Application.Interface;

namespace VSphere.Controllers
{
    public class VMController : Controller
    {
        private readonly IVMApplication _vmApplication;

        public VMController(IVMApplication vmApplication)
        {
            _vmApplication = vmApplication;
        }

        public IActionResult Index()
        {
            return View(_vmApplication.GetAll());
        }
    }
}