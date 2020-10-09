using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VSphere.Models;

namespace VSphere.Controllers
{
    public class SnapshotController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var all = new SnapshotViewModel().GetAll();
            return View(all);
        }
    }
}
