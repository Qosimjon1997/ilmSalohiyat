using ilmSalohiyat.Data;
using ilmSalohiyat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ilmSalohiyat.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
