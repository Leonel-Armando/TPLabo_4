using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TPLabo_4.Data;
using TPLabo_4.Models;
using TPLabo_4.ViewsModels;

namespace TPLabo_4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var carpinteriasSinStock = _context.carpinterias.Where(c => !c.Stock).ToList();
            var ferreteriasSinStock = _context.ferreterias.Where(f => !f.Stock).ToList();
            var modeloVista = new HomeViewModel
            {
                CarpinteriasSinStock = carpinteriasSinStock,
                FerreteriasSinStock = ferreteriasSinStock
            };

            return View(modeloVista);
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