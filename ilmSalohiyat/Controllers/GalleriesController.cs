using ilmSalohiyat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ilmSalohiyat.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: GalleriesController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Galleries.ToListAsync());
        }

    }
}
