﻿using ilmSalohiyat.Data;
using Microsoft.AspNetCore.Mvc;

namespace ilmSalohiyat.Controllers
{
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