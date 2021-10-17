using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ilmSalohiyat.Controllers
{
    public class GalleriesController : Controller
    {
        // GET: GalleriesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GalleriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GalleriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GalleriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GalleriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GalleriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GalleriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GalleriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
