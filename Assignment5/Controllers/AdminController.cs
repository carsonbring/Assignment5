using Microsoft.AspNetCore.Mvc;
using Assignment5.Models;
using Assignment5.Data;

namespace Assignment5.Controllers
{
    public class AdminController : Controller
    {
       
        //Opens the default Index page
        public IActionResult Index()
        {
          
            return View();
        }
        //links to Artist CRUD Index
        public IActionResult ManageArtists()
        {

            return RedirectToAction("Index", "Artists");
        }
        //links to Song CRUD Index
        public IActionResult ManageSongs()
        {

            return RedirectToAction("Index", "Songs");
        }
    }
}
