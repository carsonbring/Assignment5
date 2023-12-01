using Microsoft.AspNetCore.Mvc;

namespace Assignment5.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ManageArtists()
        {

            return RedirectToAction("Index", "Artists");
        }

        public IActionResult ManageSongs()
        {

            return RedirectToAction("Index", "Songs");
        }
    }
}
