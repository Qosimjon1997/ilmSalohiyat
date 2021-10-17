using ilmSalohiyat.Areas.Manager.ViewModels.Galary;
using ilmSalohiyat.Data;
using ilmSalohiyat.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ilmSalohiyat.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class GalleriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GalleriesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        // GET: GalleriesController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Galleries.ToListAsync());
        }

        // GET: Galleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.Id == id);
            var galleryViewModel = new GalaryViewModel()
            {
                Id = gallery.Id,
                Name = gallery.Name,
                ExistingImage = gallery.ImageURL
            };
            if (gallery == null)
            {
                return NotFound();
            }

            return View(galleryViewModel);
        }

        // GET: Galleries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GalaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFilename = ProcessUploadedFile(model);
                Gallery gallery = new Gallery
                {
                    Name = model.Name,
                    ImageURL = uniqueFilename
                };
                _context.Add(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Galleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);
            var galleryViewModel = new GalaryViewModel()
            {
                Id = gallery.Id,
                Name = gallery.Name,
                ExistingImage = gallery.ImageURL
            };
            if (gallery == null)
            {
                return NotFound();
            }
            return View(galleryViewModel);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GalaryViewModel model)
        {
            if(ModelState.IsValid)
            {
                var gallery = await _context.Galleries.FindAsync(model.Id);
                gallery.Name = model.Name;

                if(model.GallaryPicture != null)
                {
                    if(model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "img", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }
                    gallery.ImageURL = ProcessUploadedFile(model);
                }
                _context.Update(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Galleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.Id == id);
            var galleryViewModel = new GalaryViewModel()
            {
                Id = gallery.Id,
                Name = gallery.Name,
                ExistingImage = gallery.ImageURL
            };

            if(gallery == null)
            {
                return NotFound();
            }
            return View(galleryViewModel);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            var GalleryImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", gallery.ImageURL);
            _context.Galleries.Remove(gallery);
            if (await _context.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(GalleryImage))
                {
                    System.IO.File.Delete(GalleryImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryExists(int id)
        {
            return _context.Galleries.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile(GalaryViewModel model)
        {
            string uniqueFileName = null;

            if (model.GallaryPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.GallaryPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.GallaryPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
