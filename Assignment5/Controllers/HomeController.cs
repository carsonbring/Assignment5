using Assignment5.Data;
using Assignment5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Assignment5.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        Assignment5Context _context;
        public HomeController(ILogger<HomeController> logger, Assignment5Context context)
        {
            _context = context;

            var result = from person in _context.Artist
                         select new { person.Id, person.Name, person.Genre };
            foreach (var artist in result)
            {
                Artist art = new Artist();
                art.Name = artist.Name;
                art.Genre = artist.Genre;
                art.Id = artist.Id;
                AllPerformers.myPerformers.Add(art);
            }
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}